using System;
using System.Collections.Generic;

namespace Otc.ProjectModel.Core.Domain.Models
{
    public class Subscription
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public bool Active { get; set; }
        public IList<Payment> Payments { get; set; } = new List<Payment>();
    }
}
