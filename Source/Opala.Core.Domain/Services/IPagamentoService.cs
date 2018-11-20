using Opala.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opala.Core.Domain.Services
{
    public interface IPagamentoService
    {
        /// <summary>
        /// Inclui um pagamento do tipo PayPal
        /// </summary>
        /// <param name="pagamentoPayPal"></param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ClienteCoreException">Quando o cliente não for encontrado.</exception>
        /// <exception cref="Exceptions.AssinaturaCoreException">Quando a assinatura não for encontrada.</exception>
        /// <exception cref="System.ArgumentNullException">Excessão lançada pelo Sistema</exception>
        Task IncluiPagamentoPayPalAsync(PayPal pagamentoPayPal);

        /// <summary>
        /// Inclui um pagamento do tipo Cartao de Crédito
        /// </summary>
        /// <param name="pagamentoCartaoCredito"></param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ClienteCoreException">Quando o cliente não for encontrado.</exception>
        /// <exception cref="Exceptions.AssinaturaCoreException">Quando a assinatura não for encontrada.</exception>
        /// <exception cref="System.ArgumentNullException">Excessão lançada pelo Sistema</exception>
        Task IncluiPagamentoCartaoCreditoAsync(CartaoCredito pagamentoCartaoCredito);

        /// <summary>
        /// Retorna um Pagamento do tipo PayPal
        /// </summary>
        /// <param name="clienteId">Identificador do Cliente</param>
        /// <param name="assinaturaId">Identificador da Assinatura</param>
        /// <param name="pagamentoId">Identificador do Pagamento</param>
        /// <exception cref="Exceptions.ClienteCoreException">Quando o cliente não for encontrado.</exception>
        /// <exception cref="Exceptions.AssinaturaCoreException">Quando a assinatura não for encontrada.</exception>
        /// <returns></returns>
        Task<PayPal> RetornaPagamentoPayPalAsync(Guid clienteId, Guid assinaturaId, Guid pagamentoId);

        /// <summary>
        /// Retorna um Pagamento do tipo Credit Card
        /// </summary>
        /// <param name="clienteId">Identificador do Cliente</param>
        /// <param name="assinaturaId">Identificador da Assinatura</param>
        /// <param name="pagamentoId">Identificador do Pagamento</param>
        /// <exception cref="Exceptions.ClienteCoreException">Quando o cliente não for encontrado.</exception>
        /// <exception cref="Exceptions.AssinaturaCoreException">Quando a assinatura não for encontrada.</exception>
        /// <returns></returns>
        Task<CartaoCredito> RetornaPagamentoCartaoCreditoAsync(Guid clienteId, Guid assinaturaId, Guid pagamentoId);

        /// <summary>
        /// Lista os Pagamentos de uma Assinatura
        /// </summary>
        /// <param name="clienteId">Identificador do Cliente</param>
        /// <param name="assinaturaId">Identificador da Assinatura</param>
        /// <returns></returns>
        Task<IEnumerable<Pagamento>> RetornaPagamentosAssinaturaAsync(Guid clienteId, Guid assinaturaId);
    }
}