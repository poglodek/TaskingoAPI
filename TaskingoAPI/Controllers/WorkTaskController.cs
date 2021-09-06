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
        [HttpPost]
        public ActionResult<int> AddWorkTask([FromBody] WorkTaskCreatedDto workTaskCreatedDto)
        {
            var id = _workTaskServices.CreateNewTask(workTaskCreatedDto);
            return Created($"/WorkTask/{id}", null);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            _workTaskServices.DeleteTaskById(id);
            return NoContent();
        }

    }
}
