using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using TaskingoAPI.Dto.User;
using TaskingoAPI.Services.IRepositories;

namespace TaskingoAPI.Controllers
{
    [ApiController]
    [Route("/User")]
    [Authorize]
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
            var users = _userServices.GetAllUserDto();
            return Ok(users);
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult<string> Login([FromBody] UserLoginDto userLoginDto)
        {
            var token = _userServices.LoginUser(userLoginDto);
            return Ok(token);
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("Register")]
        public ActionResult Register([FromBody] UserCreatedDto userCreatedDto)
        {
            var id = _userServices.RegisterUser(userCreatedDto);
            return Created($"/user/{id}", null);
        }
        [AllowAnonymous]
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
        [HttpGet("GetMyId")]
        public ActionResult<int> GetMyId()
        {
            var id = _userServices.GetMyId();
            return Ok(id);
        }
        [HttpGet("GetMe")]
        public ActionResult<string> GetMyInfo()
        {
            var user = _userServices.GetMyInfo();
            return Ok(user);
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete("{id}")]
        public ActionResult DeActiveUser([FromRoute] int id)
        {
            _userServices.DeActiveUserById(id);
            return NoContent();
        }
        [HttpGet("{id}")]
        public ActionResult Get([FromRoute] int id)
        {
            var user = _userServices.GetUserDtoBy(id);
            return Ok(user);
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpPatch]
        public ActionResult Update(UserUpdateDto userUpdateDto)
        {
            _userServices.UpdateUser(userUpdateDto);
            return Ok();
        }
    }
}
