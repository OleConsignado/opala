using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Core.Test.Mock
{
    public class FakeSubscriptionRepository : ISubscriptionReadOnlyRepository
    {
        public async Task AddSubscriptionAsync(Subscription subscription)
        {
            throw new NotImplementedException();
        }

        public async Task<Subscription> GetSubscriptionAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
