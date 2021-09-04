using Microsoft.AspNetCore.Mvc;

namespace TaskingoAPI.Controllers
{
    [ApiController]
    [Route("/Task")]
    public class TaskController : ControllerBase
    {
        [HttpGet("GetAll")]
        public ActionResult<string> GetClientsList()
        {
            var users = "All Users";
            return Ok(users);
        }

    }
}
