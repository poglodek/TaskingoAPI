namespace TaskingoAPI.Services.IRepositories
{
    public interface IChatServices
    {
        void SetUserIdChat(string chatUserId);
        void Disconnect(string chatUserId);
        void SendMessage(string message, int userId);
    }
}