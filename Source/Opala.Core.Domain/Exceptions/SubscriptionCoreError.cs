using Otc.DomainBase.Exceptions;

namespace Opala.Core.Domain.Exceptions
{
    public class SubscriptionCoreError : CoreError
    {
        protected SubscriptionCoreError(string key, string message) : base(key, message)
        {
        }

        public static readonly SubscriptionCoreError PaymentNotFound = new SubscriptionCoreError("400.001", "Não existe pagamento para essa assinatura");
        public static readonly SubscriptionCoreError SubscriptionNotFound = new SubscriptionCoreError("400.002", "Assinatura não encontrada.");
        public static readonly SubscriptionCoreError SubscriptionIsActive = new SubscriptionCoreError("400.003", "Você já tem uma assinatura ativa.");
        public static readonly SubscriptionCoreError SubscriptionsEmpty = new SubscriptionCoreError("400.004", "Não existem Assinaturas para este Cliente.");
    }
}
