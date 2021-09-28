using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskingoAPI.Dto.User;
using TaskingoAPI.Dto.WorkTask;
using TaskingoAPI.Services.IRepositories;

namespace TaskingoAPI.Controllers
{
    [ApiController]
    [Route("/WorkTask")]
    [Authorize]
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
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public ActionResult<int> AddWorkTask([FromBody] WorkTaskCreatedDto workTaskCreatedDto)
        {
            var id = _workTaskServices.CreateNewTask(workTaskCreatedDto);
            _autoAssignServices.AutoAssign();
            return Created($"/WorkTask/{id}", null);
        }
        [Authorize(Roles = "Admin,Manager")]
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
            var task = _workTaskServices.GetWorkTaskDtoById(id);
            return Ok(task);
        }
        [HttpGet("GetMyTasks")]
        public ActionResult<List<WorkTaskDto>> GetMyTasks()
        {
            var user = _workTaskServices.GetMyTasks();
            return Ok(user);
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpPatch]
        public ActionResult Update([FromBody] WorkTaskUpdateDto workTaskUpdateDto)
        {
            _workTaskServices.UpdateWorkTask(workTaskUpdateDto);
            _autoAssignServices.AutoAssign();
            return Ok();
        }
    }
}
