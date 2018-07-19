using Otc.ProjectModel.Core.Domain.Exceptions;
using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Repositories;
using Otc.ProjectModel.Core.Domain.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Core.Application.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionReadOnlyRepository subscriptionRepository;

        public SubscriptionService(ISubscriptionReadOnlyRepository subscriptionRepository)
        {
            this.subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository));
        }

        public async Task AddSubscriptionAsync(Client client, Subscription subscription)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (subscription == null)
                throw new ArgumentNullException(nameof(subscription));

            var hasSubscriptionActive = client.Subscriptions.Any(c => c.Active);

            if (hasSubscriptionActive)
                throw new SubscriptionCoreException().AddError(SubscriptionCoreError.SubscriptionIsActive);

            await subscriptionRepository.AddSubscriptionAsync(client.Id, subscription);
        }

        public async Task<Subscription> GetSubcriptionAsync(Guid id)
        {
            return await subscriptionRepository.GetSubscriptionAsync(id);
        }
    }
}
