using Otc.DomainBase.Exceptions;

namespace Opala.Core.Domain.Exceptions
{
    public class ClientCoreException : CoreException<ClientCoreError>
    {
        public ClientCoreException() : base()
        {

        }

        public ClientCoreException(params ClientCoreError[] errors)
        {
            AddError(errors);
        }
    }
}
