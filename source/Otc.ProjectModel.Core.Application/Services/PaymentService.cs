using Otc.ProjectModel.Core.Domain.Exceptions;
using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Repositories;
using Otc.ProjectModel.Core.Domain.Services;
using Otc.Validations.Helpers;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Core.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IClientService clientService;
        private readonly ISubscriptionService subscriptionService;
        private readonly IPaymentWriteOnlyRepository paymentWriteOnlyRepository;

        public PaymentService(IClientService clientService, ISubscriptionService subscriptionService, IPaymentWriteOnlyRepository paymentWriteOnlyRepository)
        {
            this.clientService = clientService ?? throw new System.ArgumentNullException(nameof(clientService));
            this.subscriptionService = subscriptionService ?? throw new System.ArgumentNullException(nameof(subscriptionService));
            this.paymentWriteOnlyRepository = paymentWriteOnlyRepository ?? throw new System.ArgumentNullException(nameof(paymentWriteOnlyRepository));
        }

        private async Task ValidateEntry(Payment payment)
        {
            if (payment == null)
                throw new System.ArgumentNullException(nameof(payment));

            var client = await clientService.ClientExistsAsync(payment.ClientId);

            if (!client)
                throw new ClientCoreException(ClientCoreError.ClientNotFound);

            var subscription = await subscriptionService.GetSubcriptionAsync(payment.SubscriptionId);

            if (subscription == null)
                throw new SubscriptionCoreException(SubscriptionCoreError.SubscriptionNotFound);
        }

        public async Task AddCreditCardPaymentAsync(CreditCardPayment payment)
        {
            await ValidateEntry(payment);

            ValidationHelper.ThrowValidationExceptionIfNotValid(payment);

            await paymentWriteOnlyRepository.AddCreditCardPaymentAsync(payment);
        }

        public async Task AddPayPalPaymentAsync(PayPalPayment payment)
        {
            await ValidateEntry(payment);

            ValidationHelper.ThrowValidationExceptionIfNotValid(payment);

            await paymentWriteOnlyRepository.AddPayPalPaymentAsync(payment);
        }
    }
}
