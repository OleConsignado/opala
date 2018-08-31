using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Otc.ProjectModel.Core.Domain.Models;

namespace Otc.ProjectModel.Core.Domain.Services
{
    public interface ISubscriptionService
    {
        /// <summary>
        /// Inclui uma nova Assinatura para o Cliente.
        /// </summary>
        /// <param name="subscription"></param>
        /// <exception cref="Exceptions.ClientCoreException">Quando o cliente não existir</exception>
        /// <exception cref="Exceptions.SubscriptionCoreException">Quando a assinatura não possuir uma forma de pagamento.</exception>
        /// <exception cref="ArgumentNullException">Excessão de sistema</exception>
        Task AddSubscriptionAsync(Subscription subscription);

        /// <summary>
        /// Retorna uma Assinatura através do seu identificador
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.SubscriptionCoreException">Quando a assinatura não for encontrada.</exception>
        Task<Subscription> GetSubcriptionAsync(Guid id);

        /// <summary>
        /// Lista todas as Assinaturas de um Cliente
        /// </summary>
        /// <param name="clientId">Identificador do Cliente</param>
        /// <param name="page">Página</param>
        /// <param name="count">Quantidade de registros por página</param>
        /// <returns></returns>
        Task<IEnumerable<Subscription>> GetClientSubscriptionsAsync(Guid clientId, int page, int count);
    }
}
