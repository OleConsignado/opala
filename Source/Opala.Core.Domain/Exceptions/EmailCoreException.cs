using Otc.DomainBase.Exceptions;

namespace Opala.Core.Domain.Exceptions
{
    public class EmailCoreException : CoreException<EmailCoreError>
    {
        public EmailCoreException() : base()
        {

        }

        public EmailCoreException(params EmailCoreError[] errors)
        {
            AddError(errors);
        }
    }
}
