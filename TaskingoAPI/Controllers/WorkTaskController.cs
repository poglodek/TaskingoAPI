using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskingoAPI.Dto.User;
using TaskingoAPI.Dto.WorkTask;
using TaskingoAPI.Services.IRepositories;

namespace TaskingoAPI.Controllers
{
    [ApiController]
    [Route("/WorkTask")]
    public class WorkTaskController : ControllerBase
    {
        private readonly IWorkTaskServices _workTaskServices;
        private readonly IAutoAssignServices _autoAssignServices;

        public WorkTaskController(IWorkTaskServices workTaskServices,
            IAutoAssignServices autoAssignServices)
        {
            _workTaskServices = workTaskServices;
            _autoAssignServices = autoAssignServices;
        }
        [HttpGet("{month}/{year}")]
        public ActionResult<List<WorkTaskDto>> GetByMonth([FromRoute] int month, [FromRoute] int year)
        {
            var tasks = _workTaskServices.GetTaskByMonth(month, year);
            return Ok(tasks);
        }
        [HttpPost("Complete")]
        public ActionResult CompleteWorkTask([FromBody] WorkTaskCompletedDto workTaskCompletedDto)
        {
            _workTaskServices.CompleteWorkTask(workTaskCompletedDto);
            _autoAssignServices.AutoAssign();
            return Ok();
        }
        [HttpPost]
        public ActionResult<int> AddWorkTask([FromBody] WorkTaskCreatedDto workTaskCreatedDto)
        {
            var id = _workTaskServices.CreateNewTask(workTaskCreatedDto);
            _autoAssignServices.AutoAssign();
            return Created($"/WorkTask/{id}", null);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _workTaskServices.DeleteTaskById(id);
            _autoAssignServices.AutoAssign();
            return NoContent();
        }
        [HttpGet("{id}")]
        public ActionResult Get([FromRoute] int id)
        {
            var user = _workTaskServices.GetWorkTaskDtoById(id);
            return Ok(user);
        }
        [HttpPatch]
        public ActionResult Update([FromBody] WorkTaskUpdateDto workTaskUpdateDto)
        {
            _workTaskServices.UpdateWorkTask(workTaskUpdateDto);
            _autoAssignServices.AutoAssign();
            return Ok();
        }
        [HttpGet("test")]
        public ActionResult<List<UserDto>> test()
        {
            _autoAssignServices.AutoAssign();
            return Ok();
        }
    }
}
