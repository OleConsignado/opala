using Opala.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opala.Core.Domain.Repositories
{
    public interface ISubscriptionReadOnlyRepository
    {
        Task<Subscription> GetSubscriptionAsync(Guid id);

        Task<IEnumerable<Subscription>> GetClientSubscriptionsAsync(Guid clientId, int page, int count);
    }
}
