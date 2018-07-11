using Otc.ProjectModel.Core.Domain.Models;
using System;

namespace Otc.ProjectModel.Core.Domain.Repositories
{
    public interface IClientRepository
    {
        bool EmailExists(string email);
        void AddClient(Client client);
        Client GetClient(Guid clientId);
        void AddClientSubscription(Client client);
    }
}
