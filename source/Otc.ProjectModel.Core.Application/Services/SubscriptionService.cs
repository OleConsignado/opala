using Otc.ProjectModel.Core.Domain.Adapters;
using Otc.ProjectModel.Core.Domain.Exceptions;
using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Repositories;
using Otc.ProjectModel.Core.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Core.Application.Services
{
    public class SubscriptionService :ISubscriptionService
    {
        private readonly ISubscriptionReadOnlyRepository subscriptionReadOnlyRepository;
        private readonly ISubscriptionWriteOnlyRepository subscriptionWriteOnlyRepository;
        private readonly IClientReadOnlyRepository clientReadOnlyRepository;
        private readonly INotificationAdapter notificationAdapter;

        public SubscriptionService(ISubscriptionReadOnlyRepository subscriptionReadOnlyRepository, ISubscriptionWriteOnlyRepository subscriptionWriteOnlyRepository, IClientReadOnlyRepository clientReadOnlyRepository, INotificationAdapter notificationAdapter)
        {
            this.subscriptionReadOnlyRepository = subscriptionReadOnlyRepository ?? throw new ArgumentNullException(nameof(subscriptionReadOnlyRepository));
            this.subscriptionWriteOnlyRepository = subscriptionWriteOnlyRepository ?? throw new ArgumentNullException(nameof(subscriptionWriteOnlyRepository));
            this.clientReadOnlyRepository = clientReadOnlyRepository ?? throw new ArgumentNullException(nameof(clientReadOnlyRepository));
            this.notificationAdapter = notificationAdapter ?? throw new ArgumentNullException(nameof(notificationAdapter));
        }

        public async Task AddSubscriptionAsync(Subscription subscription)
        {
            if (subscription == null)
                throw new ArgumentNullException(nameof(subscription));

            var client = await clientReadOnlyRepository.GetClientWithSubscriptionsAsync(subscription.ClientId);

            if (client == null)
                throw new ClientCoreException(ClientCoreError.ClientNotFound);

            var hasSubscriptionActive = client.Subscriptions.Any(c => c.Active);

            if (hasSubscriptionActive)
                throw new SubscriptionCoreException().AddError(SubscriptionCoreError.SubscriptionIsActive);

            await subscriptionWriteOnlyRepository.AddSubscriptionAsync(subscription);

            //await notificationAdapter.SendAsync(client.PhoneNumber, "Assinatura incluída com sucesso");
        }

        public async Task<IEnumerable<Subscription>> GetClientSubscriptionsAsync(Guid clientId)
        {
            var client = await clientReadOnlyRepository.GetClientAsync(clientId);

            if (client == null)
                throw new ClientCoreException(ClientCoreError.ClientNotFound);

            var subscriptions = await subscriptionReadOnlyRepository.GetClientSubscriptionsAsync(clientId);

            return subscriptions;
        }

        public async Task<Subscription> GetSubcriptionAsync(Guid id)
        {
            return await subscriptionReadOnlyRepository.GetSubscriptionAsync(id);
        }
    }
}
