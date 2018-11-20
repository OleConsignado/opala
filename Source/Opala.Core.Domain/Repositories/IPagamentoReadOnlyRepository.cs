using Opala.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opala.Core.Domain.Repositories
{
    public interface IPagamentoReadOnlyRepository
    {
        Task<Pagamento> RetornaPagamentoAsync(Guid clienteId, Guid assinaturaId, Guid pagamentoId);

        Task<IEnumerable<Pagamento>> RetornaPagamentosAssinaturaAsync(Guid clienteId, Guid assinaturaId);
    }
}
