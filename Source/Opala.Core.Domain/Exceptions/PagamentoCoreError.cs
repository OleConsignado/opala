using Otc.DomainBase.Exceptions;

namespace Opala.Core.Domain.Exceptions
{
    public class PagamentoCoreError : CoreError
    {
        protected PagamentoCoreError(string key, string message) : base(key, message)
        {
        }

        public static readonly PagamentoCoreError PagamentoNaoEncontrado = new PagamentoCoreError("400.001", "Pagamento não encontrado.");
    }
}
