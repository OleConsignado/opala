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
            this.emailAdapterConfiguration = emailAdapterConfiguration ?? throw new ArgumentNullException(nameof(emailAdapterConfiguration));
        }

        public async Task EnviaAsync(string origem, string destino, string assunto, string mensagem)
        {
            var smtpClient = new SmtpClient
            {
                Host = emailAdapterConfiguration.Smtp,
                Port = emailAdapterConfiguration.Porta,
                EnableSsl = emailAdapterConfiguration.HabilitaSsl,
                Credentials = new NetworkCredential(destino, Convert.FromBase64String(emailAdapterConfiguration.Senha).ToString())
            };

            using (var message = new MailMessage(destino, origem)
            {
                Subject = assunto,
                Body = mensagem
            })
            {
                await smtpClient.SendMailAsync(message);
            }
        }
    }
}