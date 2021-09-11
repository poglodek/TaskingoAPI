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
        private readonly IPasswordServices _passwordServices;
        private readonly string ServiceMail = "YourEmail@email.com";
        private readonly string ServiceMailPassword = "Pa$$w0rd1234.";
        private readonly string ServiceMailServer = "smtp.poczta.onet.pl";
        private readonly int ServiceMailServerPort = 465;
        public MailServices(TaskingoDbContext taskingoDbContext,
            IPasswordServices passwordServices)
        {
            _taskingoDbContext = taskingoDbContext;
            _passwordServices = passwordServices;
        }
        public void ForgotPassword(string email, User user)
        {
            var smtpClient = new SmtpClient(ServiceMailServer, ServiceMailServerPort);
            smtpClient.Credentials = new NetworkCredential(ServiceMail, ServiceMailPassword);
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
            mail.From = new MailAddress(ServiceMail, "Taskingo");
            mail.To.Add(new MailAddress(ServiceMail));
            mail.CC.Add(email);
            return mail;
        }
    }
}
