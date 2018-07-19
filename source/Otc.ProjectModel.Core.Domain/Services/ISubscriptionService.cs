using System;
using System.Threading.Tasks;
using Otc.ProjectModel.Core.Domain.Models;

namespace Otc.ProjectModel.Core.Domain.Services
{
    public interface ISubscriptionService
    {
        /// <summary>
        /// Inclui uma nova assinatura para o cliente.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="subscription"></param>
        /// <exception cref="Exceptions.SubscriptionCoreException">Quando a assinatura não possuir uma forma de pagamento.</exception>
        /// <exception cref="ArgumentNullException">Excessão de sistema</exception>
        Task AddSubscriptionAsync(Client client, Subscription subscription);
        
        /// <summary>
        /// Retorna uma assinatura através do seu identificador
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        Task<Subscription> GetSubcriptionAsync(Guid id);
    }
}
