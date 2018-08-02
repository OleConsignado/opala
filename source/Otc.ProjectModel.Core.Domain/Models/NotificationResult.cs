using System;

namespace Otc.ProjectModel.Core.Domain.Models
{
    public class NotificationResult
    {
        public Guid EnvioId { get; set; }
        public string Response { get; set; }
        public string Status { get; set; }
        public string ErroMessage { get; set; }
    }
}
