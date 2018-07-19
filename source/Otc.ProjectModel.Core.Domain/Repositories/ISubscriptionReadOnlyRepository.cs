using Otc.ProjectModel.Core.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Core.Domain.Repositories
{
    public interface ISubscriptionReadOnlyRepository
    {
        Task<Subscription> GetSubscriptionAsync(Guid id);
        Task AddSubscriptionAsync(Guid id, Subscription subscription);
    }
}
