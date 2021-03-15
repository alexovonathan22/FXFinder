using FXFinder.Core.Util.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FXFinder.Core.Util
{
    public class EmailUtil : IEmailUtil
    {
        private readonly MailSettings _mailSettings;
        private readonly ILogger<EmailUtil> _log;

        public EmailUtil(IOptions<MailSettings> mailSettings, ILogger<EmailUtil> log)
        {
            _mailSettings = mailSettings.Value;
            _log = log;
        }
        public async Task<bool> SendEmailAsync(EmailMessage message)
        {
            if (message == null)
            {
                return false;
            }
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Mail)
            };
            email.To.Add(MailboxAddress.Parse(message.ToEmail));
            email.Subject = message.Subject;
            var builder = new BodyBuilder
            {
                HtmlBody = message.Body
            };
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);

            // Send email
            try
            {
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                _log.LogInformation($"Email sent to {message.ToEmail}");
                return true;
            }
            catch (Exception)
            {
                _log.LogError($"Error occured sending email to {message.ToEmail}");

                return false;
            }
            
        }
    }

    public interface IEmailUtil
    {
        Task<bool> SendEmailAsync(EmailMessage message);
    }
}
