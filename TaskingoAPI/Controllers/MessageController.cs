using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskingoAPI.Dto.Message;
using TaskingoAPI.Dto.User;
using TaskingoAPI.Services.IRepositories;

namespace TaskingoAPI.Controllers
{
    [ApiController]
    [Route("/Message")]
    public class MessageController : ControllerBase
    {
        private readonly IChatServices _chatServices;

        public MessageController(IChatServices chatServices)
        {
            _chatServices = chatServices;
        }
        [HttpGet("{userId}/{count}")]
        public ActionResult<List<MessageDto>> GetMessage([FromRoute]int userId, [FromRoute]int count)
        {
            var messages = _chatServices.GetChat(userId, count);
            return Ok(messages);
        }
    }
}
