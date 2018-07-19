using Otc.ProjectModel.Core.Domain.Models;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Core.Domain.Services
{
    public interface IPaymentService
    {
        /// <summary>
        /// Inclui um pagamento para uma assinatura
        /// </summary>
        /// <param name="client">Cliente</param>
        /// <param name="subscription">Assinatura</param>
        /// <param name="payment">Pagamento (PayPalPayment ou CreditCardPayment)</param>
        /// <exception cref="Exceptions.ClientCoreException">Quando o cliente não for encontrado.</exception>
        /// <exception cref="Exceptions.SubscriptionCoreException">Quando a assinatura não for encontrada.</exception>
        /// <exception cref="System.ArgumentNullException">Excessão lançada pelo Sistema</exception>
        Task AddPaymentAsync(Client client, Subscription subscription, Payment payment);
    }
}