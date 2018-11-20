using Opala.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opala.Core.Domain.Services
{
    public interface IClienteService
    {
        /// <summary>
        /// Verifica se um Cliente existe.
        /// </summary>
        /// <param name="clienteId">Identificador do Cliente</param>
        /// <returns>True ou False</returns>
        Task<bool> ClienteExisteAsync(Guid clienteId);

        /// <summary>
        /// Retorna um Cliente
        /// </summary>
        /// <param name="clienteId">Identificador</param>
        /// <returns></returns>
        Task<Cliente> RetornaClienteAsync(Guid clienteId);

        /// <summary>
        /// Inclui um novo Cliente
        /// </summary>
        /// <param name="cliente">Objeto Cliente</param>
        //// <exception cref="ValidationCoreException"></exception>
        Task IncluiClienteAsync(Cliente cliente);

        /// <summary>
        /// HAbilita ou Desabilita um Cliente
        /// </summary>
        /// <param name="clienteId">Identificador</param>
        /// <param name="ativa">true ou false</param>
        /// <exception cref="Exceptions.ClienteCoreException"></exception>
        Task AtivaDesativaClienteAsync(Guid clienteId, bool ativa);

        /// <summary>
        /// Atualiza um Cliente
        /// </summary>
        /// <param name="cliente">Objeto Cliente</param>
        //// <exception cref="DomainBase.Exceptions.ValidationCoreException"></exception>
        Task AtualizaClienteAsync(Cliente cliente);

        /// <summary>
        /// Exclui um Cliente
        /// </summary>
        /// <param name="clienteId">Identificador</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ClienteCoreException"></exception>
        Task ExcluiClienteAsync(Guid clienteId);

        /// <summary>
        /// Retorna uma lista de Clientes
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Cliente>> RetornaClientesAsync();
    }
}
