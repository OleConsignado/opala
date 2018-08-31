using Otc.DomainBase.Exceptions;

namespace Otc.ProjectModel.Core.Domain.Exceptions
{
    public class PaymentCoreException : CoreException<PaymentCoreError>
    {
        public PaymentCoreException() : base()
        {

        }

        public PaymentCoreException(params PaymentCoreError[] errors)
        {
            AddError(errors);
        }
    }
}
