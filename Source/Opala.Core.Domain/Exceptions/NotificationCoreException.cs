using Otc.DomainBase.Exceptions;

namespace Opala.Core.Domain.Exceptions
{
    public class NotificationCoreException : CoreException
    {
        public override string TypeName => "NotificationCoreException";
        public override string Message => "Ocorreu algum erro ao tentar enviar o e-mail.";
    }
}