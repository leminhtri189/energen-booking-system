using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using Shared.Commons.Enum;

namespace Shared.Email
{
    public class EmailUtilities : IEmailUtilities
    {
        private readonly string _hostEmailAddress;
        private readonly string _hostEmailPassword;

        public EmailUtilities(IConfiguration config)
        {
            _hostEmailAddress = config.GetSection("MailSystem:Address").Value!;
            _hostEmailPassword = config.GetSection("MailSystem:Secret").Value!;
        }

        public async Task<bool> SendEmailAsync(string recipientAddress, string subject, string body, string smtpServer, int smtpPort, string senderAdress, string senderKey)
        {
            using (SmtpClient client = new SmtpClient(smtpServer))
            {
                MailMessage message = new MailMessage(
                    from: senderAdress,
                    to: recipientAddress,
                    subject: subject,
                    body: body
                );

                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;

                client.Credentials = new NetworkCredential(senderAdress, senderKey);
                client.Port = smtpPort;
                client.EnableSsl = true;

                try
                {
                    await client.SendMailAsync(message);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public async Task<bool> SendGoogleEmailAsync(string recipient, string subject, string body, string sender, string credential)
        {
            return await SendEmailAsync(recipient, subject, body, "smtp.gmail.com", (int)DefaultSmtpServer.Google, sender, credential);
        }

        public async Task<bool> SendGoogleEmailAsync(string recipient, string subject, string body)
        {
            return await SendEmailAsync(recipient, subject, body, "smtp.gmail.com", (int)DefaultSmtpServer.Google, _hostEmailAddress, _hostEmailPassword);
        }
    }
}
