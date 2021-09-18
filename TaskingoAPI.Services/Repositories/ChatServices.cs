using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskingoAPI.Database;
using TaskingoAPI.Database.Entity;
using TaskingoAPI.Dto.Message;
using TaskingoAPI.Services.IRepositories;

namespace TaskingoAPI.Services.Repositories
{
    public class ChatServices : IChatServices
    {
        private readonly TaskingoDbContext _taskingoDbContext;
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;
        private readonly IUserContextServices _userContextServices;

        public ChatServices(TaskingoDbContext taskingoDbContext,
            IUserServices userServices,
            IMapper mapper,
            IUserContextServices userContextServices)
        {
            _taskingoDbContext = taskingoDbContext;
            _userServices = userServices;
            _mapper = mapper;
            _userContextServices = userContextServices;
        }

        public void SetUserIdChat(string chatUserId, int userId)
        {
            var user = _userServices.GetUserById(userId);
            user.UserIdChat = chatUserId;
            user.IsOnline = true;
            _taskingoDbContext.SaveChanges();
        }

        public void Disconnect(string chatUserId)
        {
            var user = _userServices.GetUserByChatUserId(chatUserId);
            user.IsOnline = false;
            _taskingoDbContext.SaveChanges();
        }

        public void SendMessage(string message, int userId)
        {
            var sender = _userServices.GetUserById(_userContextServices.GetUserId());
            var recipient = _userServices.GetUserById(userId);
            var newMessage = new Message
            {
                UserMessage = message,
                WhoGotMessage = recipient,
                WhoSentMessage = sender
            };
            _taskingoDbContext
                .Messages
                .Add(newMessage);
            _taskingoDbContext.SaveChanges();
        }

        public List<MessageDto> GetChat(int userId, int count)
        {
            var sender = _userServices.GetUserById(_userContextServices.GetUserId());
            var recipient = _userServices.GetUserById(userId);
            var messages = _taskingoDbContext
                .Messages
                .Include(x => x.WhoGotMessage)
                .Include(x => x.WhoSentMessage)
                .Where(x => x.WhoSentMessage == sender && x.WhoGotMessage == recipient || x.WhoSentMessage == recipient && x.WhoGotMessage == sender)
               // .Reverse()
                .Take(count)
                .ToList();
            var messagesDto = _mapper.Map<List<MessageDto>>(messages);
            return messagesDto;
        }
    }
}
