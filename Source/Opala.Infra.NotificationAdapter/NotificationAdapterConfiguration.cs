using Otc.ComponentModel.DataAnnotations;

namespace Opala.Infra.NotificationAdapter
{
    public class NotificationAdapterConfiguration
    {
        [Required]
        public string NotificationUrl { get; set; }
    }
}
