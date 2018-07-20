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
        private readonly IClientService clientService;

        public SubscriptionService(ISubscriptionReadOnlyRepository subscriptionRepository, IClientService clientService)
        {
            this.subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository));
            this.clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
        }

        public async Task AddSubscriptionAsync(Subscription subscription)
        {
            if (subscription == null)
                throw new ArgumentNullException(nameof(subscription));

            var client = await clientService.GetClientAsync(subscription.ClientId);

            if (client == null)
                throw new ClientCoreException(ClientCoreError.ClientNotFound);

            var hasSubscriptionActive = client.Subscriptions.Any(c => c.Active);

            if (hasSubscriptionActive)
                throw new SubscriptionCoreException().AddError(SubscriptionCoreError.SubscriptionIsActive);

            await subscriptionRepository.AddSubscriptionAsync(subscription);
        }

        public async Task<Subscription> GetSubcriptionAsync(Guid id)
        {
            return await subscriptionRepository.GetSubscriptionAsync(id);
        }
    }
}
