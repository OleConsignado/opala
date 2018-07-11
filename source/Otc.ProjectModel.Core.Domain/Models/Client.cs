using Otc.ProjectModel.Core.Domain.Exceptions;
using Otc.ProjectModel.Core.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace Otc.ProjectModel.Core.Domain.Models
{
    public class Client : Entity
    {
        private IList<Subscription> _subscriptions;

        public string Name { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }

        public Client(string name, Email email, Address address)
        {
            Name = name;
            Email = email;
            Address = address;
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

            _subscriptions.Add(subscription);
        }
    }
}
