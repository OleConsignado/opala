using Opala.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opala.Core.Domain.Services
{
    public interface IPaymentService
    {
        /// <summary>
        /// Inclui um pagamento do tipo PayPal
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ClientCoreException">Quando o cliente não for encontrado.</exception>
        /// <exception cref="Exceptions.SubscriptionCoreException">Quando a assinatura não for encontrada.</exception>
        /// <exception cref="System.ArgumentNullException">Excessão lançada pelo Sistema</exception>
        Task AddPayPalPaymentAsync(PayPalPayment payment);

        /// <summary>
        /// Inclui um pagamento do tipo Cartao de Crédito
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ClientCoreException">Quando o cliente não for encontrado.</exception>
        /// <exception cref="Exceptions.SubscriptionCoreException">Quando a assinatura não for encontrada.</exception>
        /// <exception cref="System.ArgumentNullException">Excessão lançada pelo Sistema</exception>
        Task AddCreditCardPaymentAsync(CreditCardPayment payment);

        /// <summary>
        /// Retorna um Pagamento do tipo PayPal
        /// </summary>
        /// <param name="clientId">Identificador do Cliente</param>
        /// <param name="subscriptionId">Identificador da Assinatura</param>
        /// <param name="paymentId">Identificador do Pagamento</param>
        /// <exception cref="Exceptions.ClientCoreException">Quando o cliente não for encontrado.</exception>
        /// <exception cref="Exceptions.SubscriptionCoreException">Quando a assinatura não for encontrada.</exception>
        /// <returns></returns>
        Task<PayPalPayment> GetPayPalPaymentAsync(Guid clientId, Guid subscriptionId, Guid paymentId);

        /// <summary>
        /// Retorna um Pagamento do tipo Credit Card
        /// </summary>
        /// <param name="clientId">Identificador do Cliente</param>
        /// <param name="subscriptionId">Identificador da Assinatura</param>
        /// <param name="paymentId">Identificador do Pagamento</param>
        /// <exception cref="Exceptions.ClientCoreException">Quando o cliente não for encontrado.</exception>
        /// <exception cref="Exceptions.SubscriptionCoreException">Quando a assinatura não for encontrada.</exception>
        /// <returns></returns>
        Task<CreditCardPayment> GetCreditCardPaymentAsync(Guid clientId, Guid subscriptionId, Guid paymentId);

        /// <summary>
        /// Lista os Pagamentos de uma Assinatura
        /// </summary>
        /// <param name="clientId">Identificador do Cliente</param>
        /// <param name="subscriptionId">Identificador da Assinatura</param>
        /// <returns></returns>
        Task<IEnumerable<Payment>> GetPaymentsFromSubscriptionAsync(Guid clientId, Guid subscriptionId);
    }
}