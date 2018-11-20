using Otc.ComponentModel.DataAnnotations;

namespace Opala.Infra.NotificationAdapter
{
    public class NotificacaoAdapterConfiguration
    {
        [Required]
        public string NotificacaoUrl { get; set; }
    }
}
