using Otc.DomainBase.Exceptions;

namespace Opala.Core.Domain.Exceptions
{
    public class ClienteCoreError : CoreError
    {
        protected ClienteCoreError(string key, string message) : base(key, message)
        {
        }

        public static readonly ClienteCoreError ClienteNaoEncontrado = new ClienteCoreError("400.001", "Cliente não encontrado.");
        public static readonly ClienteCoreError ClienteExistente = new ClienteCoreError("400.002", "Cliente já cadastrado.");
    }
}
