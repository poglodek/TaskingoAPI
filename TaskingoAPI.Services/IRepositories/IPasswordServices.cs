using TaskingoAPI.Database.Entity;

namespace TaskingoAPI.Services.IRepositories
{
    public interface IPasswordServices
    {
        string NewPassword();
        public string GetPassword(User user, string password);
    }
}