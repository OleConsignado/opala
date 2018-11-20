using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Opala.Core.Domain.Models;

namespace Opala.Core.Domain.Services
{
    public interface IAssinaturaService
    {
        /// <summary>
        /// Inclui uma nova Assinatura para o Cliente.
        /// </summary>
        /// <param name="assinatura"></param>
        /// <exception cref="Exceptions.ClienteCoreException">Quando o cliente não existir</exception>
        /// <exception cref="Exceptions.AssinaturaCoreException">Quando a assinatura não possuir uma forma de pagamento.</exception>
        /// <exception cref="ArgumentNullException">Excessão de sistema</exception>
        Task IncluiAssinaturaAsync(Assinatura assinatura);

        /// <summary>
        /// Retorna uma Assinatura através do seu identificador
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.AssinaturaCoreException">Quando a assinatura não for encontrada.</exception>
        Task<Assinatura> RetornaAssinaturaAsync(Guid id);

        /// <summary>
        /// Lista todas as Assinaturas de um Cliente
        /// </summary>
        /// <param name="clienteId">Identificador do Cliente</param>
        /// <param name="pagina">Página</param>
        /// <param name="totalRegistros">Quantidade de registros por página</param>
        /// <returns></returns>
        Task<IEnumerable<Assinatura>> ListaAssinaturasClienteAsync(Guid clienteId, int pagina, int totalRegistros);
    }
}
