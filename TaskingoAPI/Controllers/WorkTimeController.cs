using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskingoAPI.Dto.WorkTime;
using TaskingoAPI.Services.IRepositories;

namespace TaskingoAPI.Controllers
{
    [ApiController]
    [Route("/WorkTime")]
    public class WorkTimeController : ControllerBase
    {
        private readonly IWorkTimeServices _workTimeServices;

        public WorkTimeController(IWorkTimeServices workTimeServices)
        {
            _workTimeServices = workTimeServices;
        }

        [HttpGet("{userId}")]
        public ActionResult<List<WorkTimeDto>> GetWorkTime([FromQuery] int userId)
        {
            var workTimes = _workTimeServices.GetWorkTimeDtoByUserId(userId);
            return Ok(workTimes);
        }

        [HttpPost("Start")]
        public ActionResult StartWork()
        {
            _workTimeServices.StartWorkingTime();
            return Ok();
        }
        [HttpPost("Stop")]
        public ActionResult StopWork()
        {
            _workTimeServices.StopWorkingTime();
            return Ok();
        }
        [HttpPost("BreakTime/{minutes}")]
        public ActionResult StartWork([FromRoute]int minutes)
        {
            _workTimeServices.AddBreakTime(minutes);
            return Ok();
        }
        [HttpGet("BreakTime")]
        public ActionResult<int> GetBreakTime()
        {
            var minutes = _workTimeServices.GetBreakTime();
            return Ok(minutes);
        }

    }
}
