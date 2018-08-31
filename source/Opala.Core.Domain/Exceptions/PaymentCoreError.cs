using Otc.DomainBase.Exceptions;

namespace Otc.ProjectModel.Core.Domain.Exceptions
{
    public class PaymentCoreError : CoreError
    {
        protected PaymentCoreError(string key, string message) : base(key, message)
        {
        }

        public static readonly PaymentCoreError PaymentNotFound = new PaymentCoreError("400.001", "Pagamento não encontrado.");
    }
}
