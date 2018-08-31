using Opala.Core.Domain.Models;
using System.Threading.Tasks;

namespace Opala.Core.Domain.Repositories
{
    public interface IPaymentWriteOnlyRepository
    {
        Task AddPayPalPaymentAsync(PayPalPayment payment);

        Task AddCreditCardPaymentAsync(CreditCardPayment payment);

    }
}
