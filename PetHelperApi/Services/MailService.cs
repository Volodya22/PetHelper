using System.Net;
using System.Net.Mail;

namespace PetHelperApi.Services
{
    public class MailService : IMail
    {
        private readonly SmtpClient _smtp = new SmtpClient("smtp.gmail.com");
        private readonly NetworkCredential _cred = new NetworkCredential("cheboxarov22@gmail.com", "zOo08u3z");

        private const int Timeout = 10000;

        public void Send(string to, string subject, string body, int timeout = Timeout)
        {
            if (string.IsNullOrEmpty(to)) return;

            var message = new MailMessage();

            _smtp.Timeout = timeout;
            _smtp.Credentials = _cred;
            _smtp.EnableSsl = true;

            message.From = new MailAddress("cheboxarov22@gmail.com");
            message.To.Add(to);
            message.Subject = subject;
            message.Body = body;

            _smtp.Send(message);
        }
    }
}