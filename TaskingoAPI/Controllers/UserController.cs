using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public ActionResult<List<UserDto>> GetClientsList()
        {
            var users = _userServices.GetAllUser();
            return Ok(users);
        }
        [HttpPost("login")]
        public ActionResult<string> login([FromBody]UserLoginDto userLoginDto)
        {
            var token = _userServices.LoginUser(userLoginDto);
            return Ok(token);
        }
        [HttpPost("register")]
        public ActionResult login([FromBody]UserCreatedDto userCreatedDto)
        {
            var id = _userServices.RegisterUser(userCreatedDto);
            return Created($"/user/{id}", null);
        }

    }
}
