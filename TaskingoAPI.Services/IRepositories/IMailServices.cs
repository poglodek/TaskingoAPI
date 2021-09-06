using System.Net.Mail;
using TaskingoAPI.Dto.Entity;

namespace TaskingoAPI.Services.IRepositories
{
    public interface IMailServices
    {
        void ForgotPassword(string email);
    }
}