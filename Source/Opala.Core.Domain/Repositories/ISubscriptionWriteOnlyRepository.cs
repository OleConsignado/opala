using Opala.Core.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Opala.Core.Domain.Repositories
{
    public interface ISubscriptionWriteOnlyRepository
    {
        Task AddSubscriptionAsync(Subscription subscription);
    }
}
