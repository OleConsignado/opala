using Otc.ComponentModel.DataAnnotations;

namespace Otc.ProjectModel.Infra.NotificationAdapter
{
    public class NotificationAdapterConfiguration
    {
        [Required]
        public string NotificationUrl { get; set; }
    }
}
