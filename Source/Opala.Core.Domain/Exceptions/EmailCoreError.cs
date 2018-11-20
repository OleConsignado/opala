using Otc.DomainBase.Exceptions;

namespace Opala.Core.Domain.Exceptions
{
    public class EmailCoreError : CoreError
    {
        protected EmailCoreError(string key, string message) : base(key, message)
        {
        }

        public static readonly EmailCoreError EmailInvalido = new EmailCoreError("400.001", "Email inválido");
    }
}
