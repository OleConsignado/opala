using Otc.ProjectModel.Core.Domain.Exceptions;
using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Repositories;
using Otc.ProjectModel.Core.Domain.Services;
using System;

namespace Otc.ProjectModel.Core.Application.Services
{
    public class ClientService : IClientService
    {
        private IEmailService _emailService;
        private IClientRepository _clientRepository;

        public ClientService(IEmailService emailService, IClientRepository clientRepository)
        {
            _emailService = emailService;
            _clientRepository = clientRepository;
        }

        public void AddClient(Client client)
        {
            _clientRepository.AddClient(client);
        }

        public void AddClientSubscription(Guid clientId, Subscription subscription)
        {
            var client = _clientRepository.GetClient(clientId);

            if (client == null)
                throw new ClientCoreException().AddError(ClientCoreError.ClientNotFound);

            client.AddSubscription(subscription);
        }

        public Client GetClient(Guid clientId)
        {
            var client = _clientRepository.GetClient(clientId);

            if (client == null)
                throw new ClientCoreException().AddError(ClientCoreError.ClientNotFound);

            return client;
        }

        public void RemoveClient(Guid cliendId)
        {
            throw new NotImplementedException();
        }

        public void UpdateClient(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
