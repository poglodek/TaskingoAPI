using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TaskingoAPI.Dto;
using TaskingoAPI.Dto.Entity;
using TaskingoAPI.Services.IRepositories;

namespace TaskingoAPI.Services.Repositories
{
    public class MailServices : IMailServices
    {
        private readonly TaskingoDbContext _taskingoDbContext;
        private readonly IUserServices _userServices;
        private const string ServiceMail = "yourMail@gmail.com";

        public MailServices(TaskingoDbContext taskingoDbContext, 
            IUserServices userServices)
        {
            _taskingoDbContext = taskingoDbContext;
            _userServices = userServices;
        }
        public void ForgotPassword(string email)
        {
            var user = _userServices.GetUserByMail(email);
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential(ServiceMail, "Password123");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            var password = _userServices.NewPassword();
            user.PasswordHashed = _userServices.GetPassword(user, password);
            _taskingoDbContext.SaveChanges();
            var mail = CreateMail(email, "Forgot Password", $"Your new  Password is: {password}");
            smtpClient.Send(mail);
        }

        public MailMessage CreateMail(string email, string subject, string body)
        {
            var mail = new MailMessage();
            mail.Subject = subject;
            mail.Body = body;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.From = new MailAddress(ServiceMail, "Taskingo");
            mail.To.Add(new MailAddress(ServiceMail));
            mail.CC.Add(email);
            return mail;
        }
    }
}
