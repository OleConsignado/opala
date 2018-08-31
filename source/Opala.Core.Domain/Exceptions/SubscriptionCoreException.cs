using Otc.DomainBase.Exceptions;

namespace Otc.ProjectModel.Core.Domain.Exceptions
{
    public class SubscriptionCoreException : CoreException<SubscriptionCoreError>
    {
        public SubscriptionCoreException() : base()
        {

        }

        public SubscriptionCoreException(params SubscriptionCoreError[] errors)
        {
            AddError(errors);
        }
    }
}
