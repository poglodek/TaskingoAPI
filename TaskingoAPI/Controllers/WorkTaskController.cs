using Microsoft.AspNetCore.Mvc;
using TaskingoAPI.Dto.WorkTask;
using TaskingoAPI.Services.IRepositories;

namespace TaskingoAPI.Controllers
{
    [ApiController]
    [Route("/WorkTask")]
    public class WorkTaskController : ControllerBase
    {
        private readonly IWorkTaskServices _workTaskServices;

        public WorkTaskController(IWorkTaskServices workTaskServices)
        {
            _workTaskServices = workTaskServices;
        }
        [HttpPost("Add")]
        public ActionResult<int> AddWorkTask([FromBody] WorkTaskCreatedDto workTaskCreatedDto)
        {
            var id = _workTaskServices.CreateNewTask(workTaskCreatedDto);
            return Created($"/WorkTask/{id}", null);
        }

    }
}
