using Otc.ComponentModel.DataAnnotations;

namespace Otc.ProjectModel.Infra.Adapter.Notification
{
    public class NotificationAdapterConfiguration
    {
        [Required]
        public string NotificationUrl { get; set; }
    }
}
