using System.Collections.Generic;
using TaskingoAPI.Dto.Message;

namespace TaskingoAPI.Services.IRepositories
{
    public interface IChatServices
    {
        void SetUserIdChat(string chatUserId,int userId);
        void Disconnect(string chatUserId);
        void SendMessage(string message, int userId);
        List<MessageDto> GetChat(int userId, int count);
    }
}