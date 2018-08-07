using Otc.ProjectModel.Core.Domain.Models;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Core.Domain.Repositories
{
    public interface IPaymentWriteOnlyRepository
    {
        Task AddPayPalPaymentAsync(PayPalPayment payment);

        Task AddCreditCardPaymentAsync(CreditCardPayment payment);

    }
}
