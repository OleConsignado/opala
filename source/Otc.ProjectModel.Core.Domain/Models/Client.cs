using Otc.ComponentModel.DataAnnotations;
using Otc.ProjectModel.Core.Domain.Exceptions;
using Otc.ProjectModel.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Otc.ProjectModel.Core.Domain.Models
{
    public class Client
    {
        private IList<Subscription> _subscriptions;

        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorKey = "400.010")]
        [MinLength(3, ErrorKey = "400.011")]
        public string Name { get; set; }

        public string Email { get; set; }

        public Address Address { get; set; }

        public Client()
        {
            _subscriptions = new List<Subscription>();
        }

        public void AddSubscription(Subscription subscription)
        {
            var hasSubscriptionActive = false;

            foreach (var sub in _subscriptions)
                if (sub.Active)
                    hasSubscriptionActive = true;

            if (hasSubscriptionActive)
                throw new ClientCoreException().AddError(ClientCoreError.SubscriptionError);

            if (!subscription.Payments.Any())
                throw new SubscriptionCoreException().AddError(SubscriptionCoreError.PaymentNotFound);

            _subscriptions.Add(subscription);
        }

        public bool HasSubscription()
        {
            return _subscriptions.Any();
        }
    }
}
