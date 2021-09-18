using System.Collections.Generic;
using TaskingoAPI.Dto.Message;
using TaskingoAPI.Dto.User;

namespace TaskingoAPI.Services.IRepositories
{
    public interface IChatServices
    {
        void SetUserIdChat(string chatUserId,int userId);
        void Disconnect(string chatUserId);
        void SendMessage(string message, string userChatId, int userId);
        List<MessageDto> GetChat(int userId, int count);
        List<UserDto> GetLastChatting();
    }
}