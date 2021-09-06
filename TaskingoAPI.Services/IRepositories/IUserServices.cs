using System.Collections.Generic;
using TaskingoAPI.Dto.Entity;
using TaskingoAPI.Dto.User;

namespace TaskingoAPI.Services.IRepositories
{
    public interface IUserServices
    {
        int RegisterUser(UserCreatedDto userDto);
        string LoginUser(UserLoginDto userLoginDto);
        List<UserDto> GetAllUser();
        public User GetUserByMail(string mail);
        void ForgotPassword(string email);
        public string NewPassword();
        public string GetPassword(User user, string password);
    }
}