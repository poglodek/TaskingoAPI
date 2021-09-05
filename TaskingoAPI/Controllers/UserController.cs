using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskingoAPI.Dto.User;

namespace TaskingoAPI.Controllers
{
    [ApiController]
    [Route("/User")]
    public class UserController : ControllerBase
    {
        public UserController()
        {
            
        }
        [HttpGet("GetAll")]
        public ActionResult<string> GetClientsList()
        {
            var users = "All Users";
            return Ok(users);
        }
        [HttpPost("login")]
        public ActionResult<string> login([FromBody]UserLoginDto userLoginDto)
        {
            
            return Ok();
        }

    }
}
