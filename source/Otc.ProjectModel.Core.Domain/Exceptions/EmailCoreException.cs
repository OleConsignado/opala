using Otc.DomainBase.Exceptions;

namespace Otc.ProjectModel.Core.Domain.Exceptions
{
    public class EmailCoreError : CoreError
    {
        protected EmailCoreError(string key, string message) : base(key, message)
        {
        }

        public static readonly EmailCoreError EmailInvalidError = new EmailCoreError("400.001", "Email inválido");
    }

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
