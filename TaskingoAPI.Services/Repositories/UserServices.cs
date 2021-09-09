using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using TaskingoAPI.Database;
using TaskingoAPI.Database.Entity;
using TaskingoAPI.Dto;
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
        private readonly IMailServices _mailServices;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IUserContextServices _userContextServices;
        private readonly IMapper _mapper;

        public UserServices(TaskingoDbContext taskingoDbContext,
            IPasswordHasher<User> passwordHasher,
            IMailServices mailServices,
            AuthenticationSettings authenticationSettings,
            IUserContextServices userContextServices,
           IMapper mapper)
        {
            _taskingoDbContext = taskingoDbContext;
            _passwordHasher = passwordHasher;
            _mailServices = mailServices;
            _authenticationSettings = authenticationSettings;
            _userContextServices = userContextServices;
            _mapper = mapper;
        }



        public int RegisterUser(UserCreatedDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            if (string.IsNullOrEmpty(user.PasswordHashed)) user.PasswordHashed = NewPassword();
            user.Role = GetDefaultRole();
            user.ActualStatus = "Free";
            user.IsActive = true;
            var hashedPassword = GetPassword(user, user.PasswordHashed);
            user.PasswordHashed = hashedPassword;
            _taskingoDbContext.Users.Add(user);
            _taskingoDbContext.SaveChanges();
            return user.Id;
        }

        public string GetPassword(User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public string GetMyName()
        {
            var user = _userContextServices.GetUser;
            var name = user.Identity.Name;
            if (name is null) throw new NotFound("User not found");
            return name;
        }
        public User GetUserById(int id)
        {
            var user = _taskingoDbContext
                .Users
                .FirstOrDefault(x => x.Id == id);
            if (user is null) throw new NotFound("User not found");
            return user;
        }

        public void DeActiveUserById(int id)
        {
            var user = GetUserById(id);
            user.IsActive = false;
            _taskingoDbContext.SaveChanges();
        }

        public UserDto GetUserDtoBy(int id)
        {
            var user = GetUserById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
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
                .Where(x => x.IsActive == true)
                .ToList();
            var usersDto = _mapper.Map<List<UserDto>>(users);
            return usersDto;
        }

        public void ForgotPassword(string email)
        {
             var user = GetUserByMail(email);
            _mailServices.ForgotPassword(email, user);
        }

        public string NewPassword()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[12];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }

        public User GetUserByMail(string mail)
        {
            var user = _taskingoDbContext
                .Users
                .Where(x => x.Email.ToUpper().Equals(mail.ToUpper()))
                .FirstOrDefault();
            if (user is null) throw new NotFound("User not found");
            return user;
        }
        private Role GetDefaultRole()
        {
            return _taskingoDbContext.Roles.First();
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
    }
}
