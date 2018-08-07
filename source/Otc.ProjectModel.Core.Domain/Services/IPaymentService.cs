using Otc.ProjectModel.Core.Domain.Models;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Core.Domain.Services
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

    }
}