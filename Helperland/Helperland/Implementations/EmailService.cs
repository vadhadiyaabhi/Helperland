using Helperland.IRepositories;
using Helperland.ViewModel;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Helperland.Implementations
{
    public class EmailService : IEmailService
    {
        private const string templatePath = @"EmailTemplates/{0}.html";

        public EmailConfig SmtpConfig;

        public EmailService(IOptions<EmailConfig> _smtpConfig)
        {
            SmtpConfig = _smtpConfig.Value;
        }

        public async Task SendTestEmail(UserEmailOptions userEmailOptions)
        { 
            userEmailOptions.Body = UpdatePlaceHolders(GetBody(userEmailOptions.templateName), userEmailOptions.Placeholder);

            await SendEmail(userEmailOptions);
        }


        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mail = new MailMessage
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(SmtpConfig.SenderAddress, SmtpConfig.SenderDisplayName),
                IsBodyHtml = SmtpConfig.IsBodyHtml,
            };

            foreach(var toEmail in userEmailOptions.ToEmails)
            {
                mail.To.Add(toEmail);
            }

            NetworkCredential networkCredential = new NetworkCredential(SmtpConfig.SenderAddress, SmtpConfig.Password);

            SmtpClient smtpClient = new SmtpClient
            {
                Host = SmtpConfig.Host,
                Port = SmtpConfig.port,
                EnableSsl = SmtpConfig.EnableSSL,
                UseDefaultCredentials = SmtpConfig.UserDefaultCredentials,
                Credentials = networkCredential
            };

            mail.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mail);
        } 

        private string GetBody(string templateName)
        {
            var Body = File.ReadAllText(string.Format(templatePath, templateName));
            return Body;
        }

        private string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if(!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach(var placeholder in keyValuePairs)
                {
                    if (text.Contains(placeholder.Key))
                    {
                        text = text.Replace(placeholder.Key, placeholder.Value);
                    }
                }
            }

            return text;
        }
    }
}
