using Opala.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opala.Core.Domain.Repositories
{
    public interface IClienteReadOnlyRepository
    {
        Task<Cliente> RetornaClienteAsync(Guid clienteId);

        Task<Cliente> RetornaClienteSemAssinaturaAsync(Guid clienteId);

        Task<bool> ClienteExisteAsync(Guid clienteId);

        Task<IEnumerable<Cliente>> ListaClientesAsync();
    }
}
