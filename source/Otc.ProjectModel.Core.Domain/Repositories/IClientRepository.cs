using Otc.ProjectModel.Core.Domain.Models;
using System;

namespace Otc.ProjectModel.Core.Domain.Repositories
{
    public interface IClientRepository
    {
        void AddClient(Client client);
        Client GetClient(Guid clientId);
        void RemoveClient(Guid clientId);
        void UpdateClient(Client client);
    }
}
