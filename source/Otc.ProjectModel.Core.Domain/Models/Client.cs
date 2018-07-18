using Otc.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Otc.ProjectModel.Core.Domain.Models
{
    public class Client
    {
        public virtual IList<Subscription> Subscriptions { get; set; } = new List<Subscription>();

        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorKey = "400.001")]
        [MinLength(3, ErrorKey = "400.002")]
        public string Name { get; set; }

        [EmailAddress(ErrorKey = "400.003")]
        public string Email { get; set; }

        public Address Address { get; set; }

        public bool HasSubscription()
        {
            return Subscriptions.Any();
        }
    }
}
