using Otc.ProjectModel.Core.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Core.Domain.Repositories
{
    public interface IClientWriteOnlyRepository
    {
        Task AddClientAsync(Client client);

        Task RemoveClientAsync(Guid clientId);

        Task UpdateClientAsync(Client client);
    }
}