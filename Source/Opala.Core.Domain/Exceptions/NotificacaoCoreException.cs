using Otc.DomainBase.Exceptions;

namespace Opala.Core.Domain.Exceptions
{
    public class NotificacaoCoreException : CoreException
    {
        public override string TypeName => "NotificacaoCoreException";
        public override string Message => "Ocorreu algum erro ao tentar enviar o e-mail.";
    }
}