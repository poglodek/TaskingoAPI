using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TaskingoAPI.Controllers
{
    [ApiController]
    [Route("/User")]
    public class UserController : ControllerBase
    {
        public UserController(ILogger<UserController> logger)
        {
            logger.LogError("test");
        }
        [HttpGet("GetAll")]
        public ActionResult<string> GetClientsList()
        {
            var users = "All Users";
            return Ok(users);
        }

    }
}
