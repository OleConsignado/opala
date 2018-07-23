using Otc.ComponentModel.DataAnnotations;
using System;

namespace Otc.ProjectModel.WebApi.Dtos
{
    public class SubscriptionRequest
    {
        [Required(ErrorKey = "400.001")]
        public Guid ClientId { get; set; }

        [Required(ErrorKey = "400.002")]
        public string Name { get; set; }

        [Required(ErrorKey = "400.003")]
        public DateTime CreatedDate { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public DateTime? ExpireDate { get; set; }

        [Required(ErrorKey = "400.004")]
        public bool Active { get; set; }

    }
}
