using Otc.DomainBase.Exceptions;

namespace Opala.Core.Domain.Exceptions
{
    public class AssinaturaCoreException : CoreException<AssinaturaCoreError>
    {
        public AssinaturaCoreException() : base()
        {

        }

        public AssinaturaCoreException(params AssinaturaCoreError[] errors)
        {
            AddError(errors);
        }
    }
}
