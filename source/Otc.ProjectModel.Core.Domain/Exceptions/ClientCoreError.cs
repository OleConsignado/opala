using Otc.DomainBase.Exceptions;

namespace Otc.ProjectModel.Core.Domain.Exceptions
{
    public class ClientCoreError : CoreError
    {
        protected ClientCoreError(string key, string message) : base(key, message)
        {
        }

        public static readonly ClientCoreError SubscriptionError = new ClientCoreError("400.001", "Você já tem uma assinatura ativa");
        public static readonly ClientCoreError ClientNotFound = new ClientCoreError("400.002", "Cliente não encontrado");
    }
}
