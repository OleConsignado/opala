using Otc.ProjectModel.Core.Domain.Models;
using System;

namespace Otc.ProjectModel.Core.Domain.Repositories
{
    public interface ISubscriptionRepository
    {
        void AddSubscription(Guid clientId, Subscription subscription);

        Subscription GetSubscription(Guid id);
    }
}
