using Otc.ProjectModel.Core.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Core.Domain.Repositories
{
    public interface IClientReadOnlyRepository
    {
        Task<Client> GetClientAsync(Guid clientId);

        Task<Client> GetClientWithSubscriptionsAsync(Guid clientId);

        Task<bool> ClientExistsAsync(Guid clientId);

    }
}
