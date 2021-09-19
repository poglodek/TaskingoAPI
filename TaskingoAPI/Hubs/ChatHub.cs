using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TaskingoAPI.Model;
using TaskingoAPI.Services.IRepositories;

namespace TaskingoAPI.Hubs
{
    
    public class ChatHub : Hub
    {
        private readonly IChatServices _chatServices;
        private readonly IUserServices _userServices;

        public ChatHub(IChatServices chatServices,
            IUserServices userServices)
        {
            _chatServices = chatServices;
            _userServices = userServices;
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            _chatServices.Disconnect(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessage(string message, int recipientId)
        {
            var sender = _userServices.GetUserByChatUserId(Context.ConnectionId);
            _chatServices.SendMessage(message,Context.ConnectionId ,recipientId);
            if (_userServices.IsUserOnline(recipientId))
            {
                var newMessage = new MessageModel($"{sender.FirstName} {sender.LastName}",message);
                await Clients.Client(_userServices.GetUserChatIdByUserId(recipientId)).SendAsync("ReceiveMessage", newMessage);
            }
                
        }
        public async Task GetMyId(int userId)
        {
            _chatServices.SetUserIdChat(Context.ConnectionId, userId);
        }
    }
    
}
