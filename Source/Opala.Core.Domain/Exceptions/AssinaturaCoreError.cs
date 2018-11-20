using Otc.DomainBase.Exceptions;

namespace Opala.Core.Domain.Exceptions
{
    public class AssinaturaCoreError : CoreError
    {
        protected AssinaturaCoreError(string key, string message) : base(key, message)
        {
        }

        public static readonly AssinaturaCoreError PagamentoNaoEncontrado = new AssinaturaCoreError("400.001", "Não existe pagamento para essa assinatura");
        public static readonly AssinaturaCoreError AssinaturaNaoEncontrada = new AssinaturaCoreError("400.002", "Assinatura não encontrada.");
        public static readonly AssinaturaCoreError AssinaturaAtiva = new AssinaturaCoreError("400.003", "Você já tem uma assinatura ativa.");
        public static readonly AssinaturaCoreError ListaVazia = new AssinaturaCoreError("400.004", "Não existem Assinaturas para este Cliente.");
    }
}
