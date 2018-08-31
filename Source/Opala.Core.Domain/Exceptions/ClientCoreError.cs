using Otc.DomainBase.Exceptions;

namespace Opala.Core.Domain.Exceptions
{
    public class ClientCoreError : CoreError
    {
        protected ClientCoreError(string key, string message) : base(key, message)
        {
        }

        public static readonly ClientCoreError ClientNotFound = new ClientCoreError("400.001", "Cliente não encontrado.");
        public static readonly ClientCoreError ClientExists = new ClientCoreError("400.002", "Cliente já cadastrado.");
    }
}
