using System;

namespace Opala.Infra.NotificationAdapter.Clients
{
    public class NotificacaoResponse
    {
        public Guid Id { get; set; }
        public string Resposta { get; set; }
        public string Status { get; set; }
        public string MensagemErro { get; set; }
    }
}
