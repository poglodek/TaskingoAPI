using System.Collections.Generic;
using TaskingoAPI.Database.Entity;
using TaskingoAPI.Dto.User;

namespace TaskingoAPI.Services.IRepositories
{
    public interface IUserServices
    {
        int RegisterUser(UserCreatedDto userDto);
        string LoginUser(UserLoginDto userLoginDto);
        List<UserDto> GetAllUser();
        User GetUserByMail(string mail);
        void ForgotPassword(string email);
        string GetMyName();
        User GetUserById(int id);
        void DeActiveUserById(int id);
        UserDto GetUserDtoBy(int id);
        UserDto GetMyInfo();
        void UpdateUser(UserUpdateDto userUpdateDto);
        User GetUserByChatUserId(string chatUserId);
        string GetUserChatIdByUserId(int userId);
        bool IsUserOnline(int userId);
    }
}