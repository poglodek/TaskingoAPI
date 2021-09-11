﻿using System.Collections.Generic;
using TaskingoAPI.Database.Entity;
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
        string GetMyName();
        public User GetUserById(int id);
        void DeActiveUserById(int id);
        UserDto GetUserDtoBy(int id);
        UserDto GetMyInfo();
        void UpdateUser(UserUpdateDto userUpdateDto);
    }
}