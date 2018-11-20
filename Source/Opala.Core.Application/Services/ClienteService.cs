using Microsoft.Extensions.Logging;
using Opala.Core.Domain.Adapters;
using Opala.Core.Domain.Exceptions;
using Opala.Core.Domain.Models;
using Opala.Core.Domain.Repositories;
using Opala.Core.Domain.Services;
using Otc.Validations.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opala.Core.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IEmailAdapter emailAdapter;
        private readonly IClienteReadOnlyRepository clienteReadOnlyRepository;
        private readonly IClienteWriteOnlyRepository clienteWriteOnlyRepository;
        private readonly ILogger logger;
        private readonly ApplicationConfiguration applicationConfiguration;

        public ClienteService(IEmailAdapter emailAdapter, IClienteReadOnlyRepository clienteReadOnlyRepository, IClienteWriteOnlyRepository clienteWriteOnlyRepository, ApplicationConfiguration applicationConfiguration, ILoggerFactory loggerFactory)
        {
            this.emailAdapter = emailAdapter ?? throw new ArgumentNullException(nameof(emailAdapter));
            this.clienteReadOnlyRepository = clienteReadOnlyRepository ?? throw new ArgumentNullException(nameof(clienteReadOnlyRepository));
            this.clienteWriteOnlyRepository = clienteWriteOnlyRepository ?? throw new ArgumentNullException(nameof(clienteWriteOnlyRepository));
            this.applicationConfiguration = applicationConfiguration ?? throw new ArgumentNullException(nameof(applicationConfiguration));
            this.logger = loggerFactory?.CreateLogger<ClienteService>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task<bool> ClienteExisteAsync(Guid clienteId)
        {
            var result = await clienteReadOnlyRepository.ClienteExisteAsync(clienteId);

            return result;
        }

        public async Task IncluiClienteAsync(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente));

            ValidationHelper.ThrowValidationExceptionIfNotValid(cliente);

            // Utilizar a gravação de logInformation somente se for realmente necessário
            // ter um acompanhamento de tudo que esta acontecendo
            logger.LogInformation("Iniciando gravação do Cliente...");
            await clienteWriteOnlyRepository.IncluiClienteAsync(cliente);
            logger.LogInformation("Cliente gravado.");

            //Todo Disable for integration tests
            //logger.LogInformation("Enviando email para o Cliente");
            //await emailAdapter.SendAsync(client.Email, applicationConfiguration.EmailFrom, "Cadastro realizado com sucesso", "Seu cadastro foi realizado com sucesso");
        }

        public async Task<Cliente> RetornaClienteAsync(Guid clienteId)
        {
            var client = await clienteReadOnlyRepository.RetornaClienteAsync(clienteId);

            return client;
        }

        public async Task AtivaDesativaClienteAsync(Guid clienteId, bool ativa)
        {
            if (!await ClienteExisteAsync(clienteId))
                throw new ClienteCoreException(ClienteCoreError.ClienteNaoEncontrado);

            await clienteWriteOnlyRepository.AtivaDesativaClienteAsync(clienteId, ativa);
        }

        public async Task AtualizaClienteAsync(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente));

            ValidationHelper.ThrowValidationExceptionIfNotValid(cliente);

            if (!await ClienteExisteAsync(cliente.Id))
                throw new ClienteCoreException(ClienteCoreError.ClienteNaoEncontrado);

            await clienteWriteOnlyRepository.AtualizaClienteAsync(cliente);
        }

        public async Task ExcluiClienteAsync(Guid clienteId)
        {
            if (!await ClienteExisteAsync(clienteId))
                throw new ClienteCoreException(ClienteCoreError.ClienteNaoEncontrado);

            await clienteWriteOnlyRepository.ExcluiClienteAsync(clienteId);
        }

        public async Task<IEnumerable<Cliente>> RetornaClientesAsync()
        {
            var clients = await clienteReadOnlyRepository.ListaClientesAsync();

            return clients;
        }
    }
}
