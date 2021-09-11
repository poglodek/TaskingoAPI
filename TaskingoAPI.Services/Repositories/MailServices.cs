using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using TaskingoAPI.Database;
using TaskingoAPI.Database.Entity;
using TaskingoAPI.Services.IRepositories;

namespace TaskingoAPI.Services.Repositories
{
    public class MailServices : IMailServices
    {
        private readonly TaskingoDbContext _taskingoDbContext;
        private readonly ConfigReader _configReader;
        private readonly IPasswordServices _passwordServices;
        public MailServices(TaskingoDbContext taskingoDbContext,
            ConfigReader configReader,
            IPasswordServices passwordServices)
        {
            _taskingoDbContext = taskingoDbContext;
            _configReader = configReader;
            _passwordServices = passwordServices;
        }
        public void ForgotPassword(string email, User user)
        {
            var smtpClient = new SmtpClient(_configReader.ServiceMailServer, _configReader.ServiceMailServerPort);
            smtpClient.Credentials = new NetworkCredential(_configReader.ServiceMail, _configReader.ServiceMailPassword);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            var password = _passwordServices.NewPassword();
            user.PasswordHashed = _passwordServices.GetPassword(user, password);
            _taskingoDbContext.SaveChanges();
            var mail = CreateMail(email, "Forgot Password", $"Your new  Password is: {password}");
            Task.Run(() =>
            {
                smtpClient.Send(mail);
            });
            
        }

        public MailMessage CreateMail(string email, string subject, string body)
        {
            var mail = new MailMessage();
            mail.Subject = subject;
            mail.Body = body;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.From = new MailAddress(_configReader.ServiceMail, "Taskingo");
            mail.To.Add(new MailAddress(_configReader.ServiceMail));
            mail.CC.Add(email);
            return mail;
        }
    }
}
