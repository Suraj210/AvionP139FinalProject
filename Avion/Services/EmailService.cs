using Avion.Helpers;
using Avion.Services.Interfaces;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Avion.Services
{
    public class EmailService:IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        public void Send(string to, string subject, string html, string from = null)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from ?? _emailSettings.From));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(_emailSettings.Server, _emailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(from ?? _emailSettings.From, _emailSettings.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
