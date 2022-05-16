using EmailModule.Entity;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailModule.Service
{
    public class EmailSenderService : EmailSenderServiceInterface
    {
        private readonly EmailConfiguration _emailConfig;
        public EmailSenderService(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }
        public async Task SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
          await  SendAsync(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("sender email address",_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = message.Content;
            
            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }
            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }
        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                    await client.SendAsync(mailMessage);
                }
                catch(Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
