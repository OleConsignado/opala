using Otc.ProjectModel.Core.Domain.Adapters;
using Otc.ProjectModel.Core.Domain.Exceptions;
using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Repositories;
using Otc.ProjectModel.Core.Domain.Services;
using Otc.Validations.Helpers;
using System;

namespace Otc.ProjectModel.Core.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IEmailAdapter emailAdapter;
        private readonly IClientRepository clientRepository;

        public ClientService(IEmailAdapter emailAdapter, IClientRepository clientRepository)
        {
            this.emailAdapter = emailAdapter;
            this.clientRepository = clientRepository;
        }

        public void AddClient(Client client)
        {
            ValidationHelper.ThrowValidationExceptionIfNotValid(client);

            clientRepository.AddClient(client);

            emailAdapter.Send(client.Email, "origem@teste.com", "Cadastro realizado com sucesso", "Seu cadastro foi realizado com sucesso");
        }

        public Client GetClient(Guid clientId)
        {
            var client = clientRepository.GetClient(clientId);

            return client;
        }

        public void RemoveClient(Guid clientId)
        {
            var client = clientRepository.GetClient(clientId);

            if (client == null)
                throw new ClientCoreException(ClientCoreError.ClientNotFound);

            clientRepository.RemoveClient(clientId);
        }

        public void UpdateClient(Client client)
        {
            ValidationHelper.ThrowValidationExceptionIfNotValid(client);

            clientRepository.UpdateClient(client);
        }
    }
}
