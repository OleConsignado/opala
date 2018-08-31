using Otc.ProjectModel.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Core.Domain.Repositories
{
    public interface IPaymentReadOnlyRepository
    {
        Task<Payment> GetPaymentAsync(Guid clientId, Guid subscriptionId, Guid paymentId);

        Task<IEnumerable<Payment>> GetPaymentsFromSubscriptionAsync(Guid clientId, Guid subscriptionId);
    }
}
