using Otc.ProjectModel.Core.Domain.Adapters;
using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Repositories;
using Otc.ProjectModel.Core.Domain.Services;
using Otc.Validations.Helpers;
using System;

namespace Otc.ProjectModel.Core.Application.Services
{
    public class ClientService : IClientService
    {
        private IEmailAdapter _emailService;
        private IClientRepository _clientRepository;

        public ClientService(IEmailAdapter emailService, IClientRepository clientRepository)
        {
            _emailService = emailService;
            _clientRepository = clientRepository;
        }

        public void AddClient(Client client)
        {
            ValidationHelper.ThrowValidationExceptionIfNotValid(client);

            _clientRepository.AddClient(client);

            _emailService.Send(client.Email, "origem@teste.com", "Cadastro realizado com sucesso", "Seu cadastro foi realizado com sucesso");
        }

        public Client GetClient(Guid clientId)
        {
            var client = _clientRepository.GetClient(clientId);

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
