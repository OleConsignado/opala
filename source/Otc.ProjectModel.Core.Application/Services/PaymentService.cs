using Otc.ProjectModel.Core.Domain.Exceptions;
using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Services;

namespace Otc.ProjectModel.Core.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IClientService clientService;
        private readonly ISubscriptionService subscriptionService;

        public PaymentService(IClientService clientService, ISubscriptionService subscriptionService)
        {
            this.clientService = clientService;
            this.subscriptionService = subscriptionService;
        }

        public void AddPayment(Client client, Subscription subscription, Payment payment)
        {
            var cli = clientService.GetClient(client.Id);

            if (cli == null)
                throw new ClientCoreException(ClientCoreError.ClientNotFound);

            var sub = subscriptionService.GetSubcription(subscription.Id);

            if (sub == null)
                throw new SubscriptionCoreException(SubscriptionCoreError.SubscriptionNotFound);

            sub.Payments.Add(payment);
        }
    }
}
