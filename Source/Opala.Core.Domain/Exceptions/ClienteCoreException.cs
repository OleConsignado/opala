using Otc.DomainBase.Exceptions;

namespace Opala.Core.Domain.Exceptions
{
    public class ClienteCoreException : CoreException<ClienteCoreError>
    {
        public ClienteCoreException() : base()
        {

        }

        public ClienteCoreException(params ClienteCoreError[] errors)
        {
            AddError(errors);
        }
    }
}
