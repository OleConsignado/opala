using Otc.ProjectModel.Core.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Core.Domain.Repositories
{
    public interface ISubscriptionWriteOnlyRepository
    {
        Task AddSubscriptionAsync(Subscription subscription);
    }
}
