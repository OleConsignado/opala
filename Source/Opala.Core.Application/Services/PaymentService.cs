using Opala.Core.Domain.Exceptions;
using Opala.Core.Domain.Models;
using Opala.Core.Domain.Repositories;
using Opala.Core.Domain.Services;
using Otc.Validations.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opala.Core.Application.Services
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

        private async Task ValidatePaymentEntry(Payment payment)
        {
            if (payment == null)
                throw new System.ArgumentNullException(nameof(payment));

            await VerifyClientExists(payment.ClientId);

            await VerifySubscriptionExists(payment.SubscriptionId);
        }

        private async Task VerifySubscriptionExists(Guid subscriptionId)
        {
            var subscription = await subscriptionService.GetSubcriptionAsync(subscriptionId);

            if (subscription == null)
                throw new SubscriptionCoreException(SubscriptionCoreError.SubscriptionNotFound);
        }

        private async Task VerifyClientExists(Guid clientId)
        {
            var client = await clientService.ClientExistsAsync(clientId);

            if (!client)
                throw new ClientCoreException(ClientCoreError.ClientNotFound);
        }

        public async Task AddCreditCardPaymentAsync(CreditCardPayment payment)
        {
            await ValidatePaymentEntry(payment);

            ValidationHelper.ThrowValidationExceptionIfNotValid(payment);

            await paymentWriteOnlyRepository.AddCreditCardPaymentAsync(payment);
        }

        public async Task AddPayPalPaymentAsync(PayPalPayment payment)
        {
            await ValidatePaymentEntry(payment);

            ValidationHelper.ThrowValidationExceptionIfNotValid(payment);

            await paymentWriteOnlyRepository.AddPayPalPaymentAsync(payment);
        }

        public async Task<PayPalPayment> GetPayPalPaymentAsync(Guid clientId, Guid subscriptionId, Guid paymentId)
        {
            await VerifyClientExists(clientId);

            await VerifySubscriptionExists(subscriptionId);

            PayPalPayment payment = await paymentReadOnlyRepository.GetPaymentAsync(clientId, subscriptionId, paymentId) as PayPalPayment;

            if (payment == null)
                throw new PaymentCoreException(PaymentCoreError.PaymentNotFound);

            return payment;
        }

        public async Task<CreditCardPayment> GetCreditCardPaymentAsync(Guid clientId, Guid subscriptionId, Guid paymentId)
        {
            await VerifyClientExists(clientId);

            await VerifySubscriptionExists(subscriptionId);

            CreditCardPayment payment = await paymentReadOnlyRepository.GetPaymentAsync(clientId, subscriptionId, paymentId) as CreditCardPayment;

            if (payment == null)
                throw new PaymentCoreException(PaymentCoreError.PaymentNotFound);

            return payment;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsFromSubscriptionAsync(Guid clientId, Guid subscriptionId)
        {
            await VerifyClientExists(clientId);
            await VerifySubscriptionExists(subscriptionId);

            var payments = await paymentReadOnlyRepository.GetPaymentsFromSubscriptionAsync(clientId, subscriptionId);

            return payments;
        }
    }
}
