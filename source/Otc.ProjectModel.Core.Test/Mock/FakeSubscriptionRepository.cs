using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Core.Test.Mock
{
    public class FakeSubscriptionRepository : ISubscriptionReadOnlyRepository, ISubscriptionWriteOnlyRepository
    {
        public Task AddSubscriptionAsync(Subscription subscription)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Subscription>> GetClientSubscriptionsAsync(Guid clientId)
        {
            throw new NotImplementedException();
        }

        public Task<Subscription> GetSubscriptionAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
