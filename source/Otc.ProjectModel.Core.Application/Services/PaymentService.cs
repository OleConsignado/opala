using Otc.ProjectModel.Core.Domain.Exceptions;
using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Repositories;
using Otc.ProjectModel.Core.Domain.Services;
using Otc.Validations.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Core.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IClientService clientService;
        private readonly ISubscriptionService subscriptionService;
        private readonly IPaymentWriteOnlyRepository paymentWriteOnlyRepository;
        private readonly IPaymentReadOnlyRepository paymentReadOnlyRepository;

        public PaymentService(IClientService clientService, ISubscriptionService subscriptionService, IPaymentWriteOnlyRepository paymentWriteOnlyRepository, IPaymentReadOnlyRepository paymentReadOnlyRepository)
        {
            this.clientService = clientService ?? throw new System.ArgumentNullException(nameof(clientService));
            this.subscriptionService = subscriptionService ?? throw new System.ArgumentNullException(nameof(subscriptionService));
            this.paymentWriteOnlyRepository = paymentWriteOnlyRepository ?? throw new System.ArgumentNullException(nameof(paymentWriteOnlyRepository));
            this.paymentReadOnlyRepository = paymentReadOnlyRepository ?? throw new ArgumentNullException(nameof(paymentReadOnlyRepository));
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

        public async Task<PayPalPayment> GetPayPalPaymentAsync(Guid paymentId)
        {
            PayPalPayment payment = await paymentReadOnlyRepository.GetPaymentAsync(paymentId) as PayPalPayment;

            return payment;
        }

        public async Task<CreditCardPayment> GetCreditCardPaymentAsync(Guid paymentId)
        {
            CreditCardPayment payment = await paymentReadOnlyRepository.GetPaymentAsync(paymentId) as CreditCardPayment;

            return payment;
        }

        public Task<IEnumerable<Payment>> GetPaymentsFromSubscriptionAsync(Guid subscriptionId)
        {
            var subscriptionExist = subscriptionService.GetSubcriptionAsync(subscriptionId);

            if (subscriptionExist == null)
                throw new SubscriptionCoreException(SubscriptionCoreError.SubscriptionNotFound);

            var payments = paymentReadOnlyRepository.GetPaymentsFromSubscriptionAsync(subscriptionId);

            return payments;
        }
    }
}
