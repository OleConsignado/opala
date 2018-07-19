using Otc.DomainBase.Exceptions;

namespace Otc.ProjectModel.Core.Domain.Exceptions
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
