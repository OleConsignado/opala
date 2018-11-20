using Opala.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opala.Core.Domain.Repositories
{
    public interface IAssinaturaReadOnlyRepository
    {
        Task<Assinatura> RetornaAssinaturaAsync(Guid id);

        Task<IEnumerable<Assinatura>> RetornaAssinaturasClienteAsync(Guid clienteId, int pagina, int totalRegistros);
    }
}
