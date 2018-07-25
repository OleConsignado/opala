using Otc.ProjectModel.Core.Domain.Exceptions;
using Otc.ProjectModel.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Core.Domain.Services
{
    public interface IClientService
    {
        //Todo Exception

        /// <summary>
        /// Verifica se um Cliente existe.
        /// </summary>
        /// <param name="clientId">Identificador do Cliente</param>
        /// <returns>True ou False</returns>
        Task<bool> ClientExists(Guid clientId);

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
        /// Exclui um Cliente
        /// </summary>
        /// <param name="clientId">Identificador</param>
        /// <exception cref="Exceptions.ClientCoreException"></exception>
        Task RemoveClientAsync(Guid clientId);

        /// <summary>
        /// Atualiza um Cliente
        /// </summary>
        /// <param name="client">Objeto Cliente</param>
        //// <exception cref="DomainBase.Exceptions.ValidationCoreException"></exception>
        Task UpdateClientAsync(Client client);

        //Todo Add HasSubscription
    }
}
