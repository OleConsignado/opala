using Opala.Core.Domain.Adapters;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Opala.Infra.EmailAdapter
{
    public class EmailAdapter : IEmailAdapter
    {
        private readonly EmailAdapterConfiguration emailAdapterConfiguration;

        public EmailAdapter(EmailAdapterConfiguration emailAdapterConfiguration)
        {
            this.emailAdapterConfiguration = emailAdapterConfiguration ?? throw new System.ArgumentNullException(nameof(emailAdapterConfiguration));
        }

        public async Task SendAsync(string to, string from, string subject, string body)
        {
            var smtpClient = new SmtpClient
            {
                Host = emailAdapterConfiguration.Smtp,
                Port = emailAdapterConfiguration.Port,
                EnableSsl = emailAdapterConfiguration.EnableSsl,
                Credentials = new NetworkCredential(from, Convert.FromBase64String(emailAdapterConfiguration.Password).ToString())
            };

            using (var message = new MailMessage(from, to)
            {
                Subject = subject,
                Body = body
            })
            {
                await smtpClient.SendMailAsync(message);
            }
        }
    }
}