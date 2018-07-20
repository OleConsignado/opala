using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Otc.ProjectModel.WebApi.Dtos
{
    public class SubscriptionRequest
    {
        public Guid ClientId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public bool Active { get; set; }

    }
}
