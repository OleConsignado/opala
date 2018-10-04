using Otc.DomainBase.Exceptions;

namespace Opala.Core.Domain.Exceptions
{
    public class PhoneNumberInvalidCoreException : CoreException
    {
        public override string TypeName => "PhoneNumberInvalidCoreException";
        public override string Message => "O número do telefone não é válido!";
    }
}