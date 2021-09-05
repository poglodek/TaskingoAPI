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
        private const string ServiceMail = "yourMail@gmail.com";

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
            if (string.IsNullOrEmpty(user.PasswordHashed)) user.PasswordHashed = NewPassword();
            user.Role = GetDefaultRole();
            user.ActualStatus = "Free";
            var hashedPassword = GetPassword(user, user.PasswordHashed);
            user.PasswordHashed = hashedPassword;
            _taskingoDbContext.Users.Add(user);
            _taskingoDbContext.SaveChanges();
            return user.Id;
        }

        private string GetPassword(User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
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

        public void ForgotPassword(string email)
        {
            var user = GetUserByMail(email);
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential(ServiceMail, "Password123");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            var mail = CreateMail(user, email);
            smtpClient.Send(mail);
        }

        private MailMessage CreateMail(User user, string email)
        {
            var mail = new MailMessage();
            mail.Subject = "Forgot Password";
            var password = NewPassword();
            user.PasswordHashed = GetPassword(user, password);
            mail.Body = "Your new  Password is: " + password;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.From = new MailAddress(ServiceMail, "Taskingo");
            mail.To.Add(new MailAddress(ServiceMail));
            mail.CC.Add(email);
            _taskingoDbContext.SaveChanges();
            return mail;
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

        private string NewPassword()
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

        private User GetUserByMail(string mail)
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

    }
}
