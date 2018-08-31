using Opala.Core.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Opala.Core.Domain.Repositories
{
    public interface IClientReadOnlyRepository
    {
        Task<Client> GetClientAsync(Guid clientId);

        Task<Client> GetClientWithSubscriptionsAsync(Guid clientId);

        Task<bool> ClientExistsAsync(Guid clientId);

    }
}
