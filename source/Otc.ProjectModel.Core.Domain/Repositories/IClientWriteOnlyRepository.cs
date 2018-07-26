using Otc.ProjectModel.Core.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Core.Domain.Repositories
{
    public interface IClientWriteOnlyRepository
    {
        Task AddClientAsync(Client client);

        Task EnableDisableClientAsync(Guid clientId, bool isActive);

        Task UpdateClientAsync(Client client);
    }
}