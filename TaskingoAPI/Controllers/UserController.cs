﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskingoAPI.Dto.User;
using TaskingoAPI.Services.IRepositories;

namespace TaskingoAPI.Controllers
{
    [ApiController]
    [Route("/User")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpGet("GetAll")]
        public ActionResult<List<UserDto>> GetAllUsers()
        {
            var users = _userServices.GetAllUser();
            return Ok(users);
        }
        [HttpPost("Login")]
        public ActionResult<string> Login([FromBody] UserLoginDto userLoginDto)
        {
            var token = _userServices.LoginUser(userLoginDto);
            return Ok(token);
        }
        [HttpPost("Register")]
        public ActionResult Register([FromBody] UserCreatedDto userCreatedDto)
        {
            var id = _userServices.RegisterUser(userCreatedDto);
            return Created($"/user/{id}", null);
        }
        [HttpGet("ForgotPassword")]
        public ActionResult ForgotPassword([FromQuery] string email)
        {
            _userServices.ForgotPassword(email);
            return Ok();
        }
        [HttpGet("GetMyName")]
        public ActionResult<string> GetMyName()
        {
            var name = _userServices.GetMyName();
            return Ok(name);
        }
        [HttpDelete]
        public ActionResult DeActiveUser([FromRoute]int id)
        {
            _userServices.DeActiveUserById(id);
            return Ok();
        }
        [HttpGet("{id}")]
        public ActionResult Get([FromRoute] int id)
        {
            var user = _userServices.GetUserDtoBy(id);
            return Ok(user);
        }
    }
}
