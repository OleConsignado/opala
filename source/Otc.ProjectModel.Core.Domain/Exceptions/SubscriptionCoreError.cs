using Otc.DomainBase.Exceptions;

namespace Otc.ProjectModel.Core.Domain.Exceptions
{
    public class SubscriptionCoreError : CoreError
    {
        protected SubscriptionCoreError(string key, string message) : base(key, message)
        {
        }

        public static readonly SubscriptionCoreError PaymentNotFound = new SubscriptionCoreError("400.001", "Não existe pagamento para essa assinatura");
    }
}
