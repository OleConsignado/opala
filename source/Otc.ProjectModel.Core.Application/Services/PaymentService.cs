using Otc.ProjectModel.Core.Domain.Exceptions;
using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Services;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Core.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IClientService clientService;
        private readonly ISubscriptionService subscriptionService;

        public PaymentService(IClientService clientService, ISubscriptionService subscriptionService)
        {
            this.clientService = clientService ?? throw new System.ArgumentNullException(nameof(clientService));
            this.subscriptionService = subscriptionService ?? throw new System.ArgumentNullException(nameof(subscriptionService));
        }

        public async Task AddPaymentAsync(Client client, Subscription subscription, Payment payment)
        {
            if (client == null)
                throw new System.ArgumentNullException(nameof(client));

            if (subscription == null)
                throw new System.ArgumentNullException(nameof(subscription));

            if (payment == null)
                throw new System.ArgumentNullException(nameof(payment));

            var cli = await clientService.GetClientAsync(client.Id);

            if (cli == null)
                throw new ClientCoreException(ClientCoreError.ClientNotFound);

            var sub = await subscriptionService.GetSubcriptionAsync(subscription.Id);

            if (sub == null)
                throw new SubscriptionCoreException(SubscriptionCoreError.SubscriptionNotFound);

            sub.Payments.Add(payment);
        }
    }
}
