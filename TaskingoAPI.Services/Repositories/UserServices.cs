using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TaskingoAPI.Database;
using TaskingoAPI.Database.Entity;
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
        private readonly IRoleServices _roleServices;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IUserContextServices _userContextServices;
        private readonly IPasswordServices _passwordServices;
        private readonly IMapper _mapper;

        public UserServices(TaskingoDbContext taskingoDbContext,
            IPasswordHasher<User> passwordHasher,
            IMailServices mailServices,
            IRoleServices roleServices,
            AuthenticationSettings authenticationSettings,
            IUserContextServices userContextServices,
            IPasswordServices passwordServices,
           IMapper mapper)
        {
            _taskingoDbContext = taskingoDbContext;
            _passwordHasher = passwordHasher;
            _mailServices = mailServices;
            _roleServices = roleServices;
            _authenticationSettings = authenticationSettings;
            _userContextServices = userContextServices;
            _passwordServices = passwordServices;
            _mapper = mapper;
        }



        public int RegisterUser(UserCreatedDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            if (string.IsNullOrEmpty(user.PasswordHashed)) user.PasswordHashed = _passwordServices.NewPassword();
            user.Role = _roleServices.GetRoleByName(userDto.Role);
            user.ActualStatus = "Offline";
            user.IsActive = true;
            var hashedPassword = _passwordServices.GetPassword(user, user.PasswordHashed);
            user.PasswordHashed = hashedPassword;
            _taskingoDbContext.Users.Add(user);
            _taskingoDbContext.SaveChanges();
            return user.Id;
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
                .Include(x => x.Role)
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

        public UserDto GetMyInfo()
        {
            var user = GetUserDtoBy(_userContextServices.GetUserId());
            return user;
        }

        public void UpdateUser(UserUpdateDto userUpdateDto)
        {
            var user = GetUserById(userUpdateDto.Id);
            user.Address = userUpdateDto.Address;
            user.FirstName = userUpdateDto.FirstName;
            user.LastName = userUpdateDto.LastName;
            user.Phone = userUpdateDto.Phone;
            user.Email = userUpdateDto.Email;
            user.Role = _roleServices.GetRoleByName(userUpdateDto.Role);
            _taskingoDbContext.SaveChanges();
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
                .Include(x => x.Role)
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

        

        public User GetUserByMail(string mail)
        {
            var user = _taskingoDbContext
                .Users
                .Where(x => x.Email.ToUpper().Equals(mail.ToUpper()))
                .FirstOrDefault();
            if (user is null) throw new NotFound("User not found");
            return user;
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
