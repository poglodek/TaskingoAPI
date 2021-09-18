using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
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
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("Conneted via hub:"+ Context.ConnectionId);
           // _chatServices.SetUserIdChat(Context.ConnectionId);
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            _chatServices.Disconnect(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessage(string message, int userId)
        {
            _chatServices.SendMessage(message, userId);
            if (_userServices.IsUserOnline(userId))
                await Clients.Client(_userServices.GetUserChatIdByUserId(userId)).SendAsync("ReceiveMessage", $"\n{message}");
        }
    }
    
}
