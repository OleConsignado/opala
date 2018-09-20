using System;

namespace Opala.Infra.NotificationAdapter.Clients
{
    public class NotificationResponse
    {
        public Guid Id { get; set; }
        public string Response { get; set; }
        public string Status { get; set; }
        public string ErroMessage { get; set; }
    }
}
