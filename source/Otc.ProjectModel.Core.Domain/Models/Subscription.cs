using Otc.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Otc.ProjectModel.Core.Domain.Models
{
    public class Subscription
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(ErrorKey = "400.001")]
        public Guid ClientId { get; set; }
        [Required(ErrorKey = "400.002")]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public bool Active { get; set; }
        public IList<Payment> Payments { get; set; } = new List<Payment>();
    }
}
