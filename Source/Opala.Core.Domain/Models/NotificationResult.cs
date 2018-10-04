using System;

namespace Opala.Core.Domain.Models
{
    public class NotificationResult
    {
        public Guid EnvioId { get; set; }
        public string Response { get; set; }
        public string ErroMessage { get; set; }
    }
}
