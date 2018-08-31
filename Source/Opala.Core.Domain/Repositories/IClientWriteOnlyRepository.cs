using Opala.Core.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Opala.Core.Domain.Repositories
{
    public interface IClientWriteOnlyRepository
    {
        Task AddClientAsync(Client client);

        Task EnableDisableClientAsync(Guid clientId, bool isActive);

        Task UpdateClientAsync(Client client);

        Task RemoveClientAsync(Guid clientId);
    }
}