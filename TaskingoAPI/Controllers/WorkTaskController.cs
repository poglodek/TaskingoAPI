using Microsoft.AspNetCore.Mvc;

namespace TaskingoAPI.Controllers
{
    [ApiController]
    [Route("/WorkTask")]
    public class WorkTaskController : ControllerBase
    {
        [HttpGet("GetAll")]
        public ActionResult<string> GetClientsList()
        {
            var users = "All Users";
            return Ok(users);
        }

    }
}
