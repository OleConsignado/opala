using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Repositories;
using System;

namespace Otc.ProjectModel.Core.Test.Mock
{
    public class FakeSubscriptionRepository : ISubscriptionRepository
    {
        public void AddSubscription(Guid clientId, Subscription subscription)
        {
            throw new NotImplementedException();
        }

        public Subscription GetSubscription(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
