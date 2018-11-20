using Opala.Core.Domain.Models;
using System.Threading.Tasks;

namespace Opala.Core.Domain.Repositories
{
    public interface IPagamentoWriteOnlyRepository
    {
        Task IncluiPagamentoPayPalAsync(PayPal pagamentoPayPal);

        Task IncluiPagamentoCartaoCreditoAsync(CartaoCredito pagamentoCartaoCredito);
    }
}
