using System;

namespace Opala.WebApi.Dtos
{
    public class AddClientSubscriptionPost
    {
        public Guid ClientId { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public DateTime? ExpireDate { get; set; }

        public bool Active { get; set; }
    }
}
