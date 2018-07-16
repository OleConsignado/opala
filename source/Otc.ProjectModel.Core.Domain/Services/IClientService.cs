using Otc.ProjectModel.Core.Domain.Models;
using System;

namespace Otc.ProjectModel.Core.Domain.Services
{
    public interface IClientService
    {
        Client GetClient(Guid clientId);
        void AddClient(Client client);
        void RemoveClient(Guid cliendId);
        void UpdateClient(Client client);
    }
}
