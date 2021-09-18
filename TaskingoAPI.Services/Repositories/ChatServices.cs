using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskingoAPI.Database;
using TaskingoAPI.Database.Entity;
using TaskingoAPI.Services.IRepositories;

namespace TaskingoAPI.Services.Repositories
{
    public class ChatServices : IChatServices
    {
        private readonly TaskingoDbContext _taskingoDbContext;
        private readonly IUserServices _userServices;
        private readonly IUserContextServices _userContextServices;

        public ChatServices(TaskingoDbContext taskingoDbContext,
            IUserServices userServices,
            IUserContextServices userContextServices)
        {
            _taskingoDbContext = taskingoDbContext;
            _userServices = userServices;
            _userContextServices = userContextServices;
        }

        public void SetUserIdChat(string chatUserId)
        {
            var user = _userServices.GetUserById(_userContextServices.GetUserId());
            user.UserIdChat = chatUserId;
            user.IsOnline = true;
            _taskingoDbContext.SaveChanges();
        }

        public void Disconnect(string chatUserId)
        {
            var user = _userServices.GetUserByChatUserId(chatUserId);
            user.IsOnline = false;
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

    }
}
