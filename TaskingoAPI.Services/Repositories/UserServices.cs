using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskingoAPI.Database.Entity;
using TaskingoAPI.Dto;
using TaskingoAPI.Dto.Entity;
using TaskingoAPI.Dto.User;
using TaskingoAPI.Exceptions;
using TaskingoAPI.Services.Authentication;
using TaskingoAPI.Services.IRepositories;

namespace TaskingoAPI.Services.Repositories
{
    public class UserServices : IUserServices
    {
        private readonly TaskingoDbContext _taskingoDbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IMapper _mapper;

        public UserServices(TaskingoDbContext taskingoDbContext,
            IPasswordHasher<User> passwordHasher,
            AuthenticationSettings authenticationSettings,
           IMapper mapper)
        {
            _taskingoDbContext = taskingoDbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _mapper = mapper;
        }

        

        public int RegisterUser(UserCreatedDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Role = GetDefaultRole();
            user.ActualStatus = "Free";
            var hashedPassword = _passwordHasher.HashPassword(user, user.PasswordHashed);
            user.PasswordHashed = hashedPassword;
            _taskingoDbContext.Users.Add(user);
            _taskingoDbContext.SaveChanges();
            return user.Id;
        }

        public string LoginUser(UserLoginDto userLoginDto)
        {
            var user = _taskingoDbContext
                .Users
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Email == userLoginDto.Email);
            if (user is null)
                throw new NotFound("User or password wrong.");
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHashed, userLoginDto.PasswordHashed);
            if (result == PasswordVerificationResult.Failed)
                throw new NotFound("User or password wrong.");

            var claims = GetClaims(user);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(_authenticationSettings.JwtExpireHours);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public List<UserDto> GetAllUser()
        {
            var users = _taskingoDbContext
                .Users
                .ToList();
            var usersDto = _mapper.Map<List<UserDto>>(users);
            return usersDto;
        }

        private List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user}")
            };
            return claims;
        }

        private Role GetDefaultRole()
        {
            return _taskingoDbContext.Roles.First();
        }
        
    }
}
