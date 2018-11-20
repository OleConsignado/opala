using Opala.Core.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Opala.Core.Domain.Repositories
{
    public interface IClienteWriteOnlyRepository
    {
        Task IncluiClienteAsync(Cliente cliente);

        Task AtivaDesativaClienteAsync(Guid clienteId, bool ativa);

        Task AtualizaClienteAsync(Cliente cliente);

        Task ExcluiClienteAsync(Guid clienteId);
    }
}