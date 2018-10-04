using System;

namespace Opala.Infra.NotificationAdapter.Clients
{
    public class NotificationResponse
    {
        public Guid Codigo { get; set; }
        public string Resp { get; set; }
        public string StatusResp { get; set; }
        public string ErroMessage { get; set; }
    }
}
