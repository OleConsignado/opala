using System;

namespace Otc.ProjectModel.Infra.NotificationAdapter
{
    public class NotificationResponse
    {
        public Guid Id { get; set; }
        public string Response { get; set; }
        public string Status { get; set; }
        public string ErroMessage { get; set; }
    }
}
