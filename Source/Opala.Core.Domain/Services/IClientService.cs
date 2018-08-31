using Opala.Core.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Opala.Core.Domain.Services
{
    public interface IClientService
    {
        //Todo Exception

        /// <summary>
        /// Verifica se um Cliente existe.
        /// </summary>
        /// <param name="clientId">Identificador do Cliente</param>
        /// <returns>True ou False</returns>
        Task<bool> ClientExistsAsync(Guid clientId);

        /// <summary>
        /// Retorna um Cliente
        /// </summary>
        /// <param name="clientId">Identificador</param>
        /// <returns></returns>
        Task<Client> GetClientAsync(Guid clientId);

        /// <summary>
        /// Inclui um novo Cliente
        /// </summary>
        /// <param name="client">Objeto Cliente</param>
        //// <exception cref="ValidationCoreException"></exception>
        Task AddClientAsync(Client client);

        /// <summary>
        /// HAbilita ou Desabilita um Cliente
        /// </summary>
        /// <param name="clientId">Identificador</param>
        /// <param name="isActive">true ou false</param>
        /// <exception cref="Exceptions.ClientCoreException"></exception>
        Task EnableDisableClientAsync(Guid clientId, bool isActive);

        /// <summary>
        /// Atualiza um Cliente
        /// </summary>
        /// <param name="client">Objeto Cliente</param>
        //// <exception cref="DomainBase.Exceptions.ValidationCoreException"></exception>
        Task UpdateClientAsync(Client client);

        /// <summary>
        /// Exclui um Cliente
        /// </summary>
        /// <param name="clientId">Identificador</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ClientCoreException"></exception>
        Task RemoveClientAsync(Guid clientId);

        //Todo Add HasSubscription
    }
}
