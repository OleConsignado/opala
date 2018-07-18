using System;
using Otc.ProjectModel.Core.Domain.Models;

namespace Otc.ProjectModel.Core.Domain.Services
{
    public interface ISubscriptionService
    {
        //Todo finalizar descrição

        /// <summary>
        /// Inclui uma nova assinatura para o cliente.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="subscription"></param>
        /// <exception cref="Exceptions.SubscriptionCoreException">Quando a assinatura não possuir uma forma de pagamento.</exception>
        void AddSubscription(Client client, Subscription subscription);
        
        /// <summary>
        /// Retorna uma assinatura através do seu identificador
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        Subscription GetSubcription(Guid id);
    }
}
