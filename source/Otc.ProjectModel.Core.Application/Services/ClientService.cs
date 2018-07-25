using Otc.ProjectModel.Core.Domain.Adapters;
using Otc.ProjectModel.Core.Domain.Exceptions;
using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Repositories;
using Otc.ProjectModel.Core.Domain.Services;
using Otc.Validations.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Core.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IEmailAdapter emailAdapter;
        private readonly IClientReadOnlyRepository clientReadOnlyRepository;
        private readonly IClientWriteOnlyRepository clientWriteOnlyRepository;
        private readonly ISubscriptionService subscriptionService;

        private readonly ApplicationConfiguration applicationConfiguration;

        public ClientService(IEmailAdapter emailAdapter, IClientReadOnlyRepository clientReadOnlyRepository, IClientWriteOnlyRepository clientWriteOnlyRepository, ApplicationConfiguration applicationConfiguration, ISubscriptionService subscriptionService)
        {
            this.emailAdapter = emailAdapter ?? throw new ArgumentNullException(nameof(emailAdapter));
            this.clientReadOnlyRepository = clientReadOnlyRepository ?? throw new ArgumentNullException(nameof(clientReadOnlyRepository));
            this.clientWriteOnlyRepository = clientWriteOnlyRepository ?? throw new ArgumentNullException(nameof(clientWriteOnlyRepository));
            this.applicationConfiguration = applicationConfiguration ?? throw new ArgumentNullException(nameof(applicationConfiguration));
            this.subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
        }

        public async Task<bool> ClientExists(Guid clientId)
        {
            var client = await GetClientAsync(clientId);

            return client != null;
        }


        public async Task AddClientAsync(Client client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            ValidationHelper.ThrowValidationExceptionIfNotValid(client);

            await clientWriteOnlyRepository.AddClientAsync(client);

            await emailAdapter.SendAsync(client.Email, applicationConfiguration.EmailFrom, "Cadastro realizado com sucesso", "Seu cadastro foi realizado com sucesso");
        }

        public async Task<Client> GetClientAsync(Guid clientId)
        {
            var client = await clientReadOnlyRepository.GetClientAsync(clientId);

            return client;
        }

        public async Task<IEnumerable<Subscription>> GetSubscriptionsAsync(Guid clientId)
        {
            var subscriptions = await subscriptionService.GetClientSubscriptionsAsync(clientId);

            return subscriptions;
        }

        public async Task RemoveClientAsync(Guid clientId)
        {
            var client = await clientReadOnlyRepository.GetClientAsync(clientId);

            if (client == null)
                throw new ClientCoreException(ClientCoreError.ClientNotFound);

            if (!await ClientExists(client.Id))
                throw new ClientCoreException(ClientCoreError.ClientNotFound);

            await clientWriteOnlyRepository.RemoveClientAsync(clientId);
        }

        public async Task UpdateClientAsync(Client client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            ValidationHelper.ThrowValidationExceptionIfNotValid(client);

            if (!await ClientExists(client.Id))
                throw new ClientCoreException(ClientCoreError.ClientNotFound);

            await clientWriteOnlyRepository.UpdateClientAsync(client);
        }
    }
}
