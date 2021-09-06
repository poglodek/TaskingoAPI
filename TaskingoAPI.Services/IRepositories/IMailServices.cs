using System.Net.Mail;
using TaskingoAPI.Database.Entity;

namespace TaskingoAPI.Services.IRepositories
{
    public interface IMailServices
    {
        void ForgotPassword(string email, User user);
    }
}