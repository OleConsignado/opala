using Otc.ProjectModel.Core.Domain.Models;
using System;

namespace Otc.ProjectModel.Core.Domain.Services
{
    public interface IClientService
    {
        /// <summary>
        /// Retorna um Cliente
        /// </summary>
        /// <param name="clientId">Identificador</param>
        /// <returns></returns>
        Client GetClient(Guid clientId);

        /// <summary>
        /// Inclui um novo Cliente
        /// </summary>
        /// <param name="client">Objeto Cliente</param>
        void AddClient(Client client);

        /// <summary>
        /// Exclui um Cliente
        /// </summary>
        /// <param name="cliendId">Identificador</param>
        void RemoveClient(Guid cliendId);

        /// <summary>
        /// Atualiza um Cliente
        /// </summary>
        /// <param name="client">Objeto Cliente</param>
        void UpdateClient(Client client);
    }
}
