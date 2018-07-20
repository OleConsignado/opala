using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Otc.ProjectModel.Core.Domain.Exceptions;
using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Services;
using Otc.ProjectModel.WebApi.Dtos;
using Otc.Validations.Helpers;

namespace Otc.ProjectModel.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService clientService;
        private readonly ISubscriptionService subscriptionService;

        public ClientsController(IClientService clientService, ISubscriptionService subscriptionService)
        {
            this.clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            this.subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
        }

        /// <summary>
        /// Retorna um Cliente com base no seu identificador
        /// </summary>
        /// <param name="clientId">Identificador</param>
        /// <returns>Client</returns>
        [HttpGet("{clientId}")]
        [ProducesResponseType(typeof(ClientCoreException), 400)]
        [ProducesResponseType(typeof(ClientResponse), 200)]
        public async Task<IActionResult> GetClientAsync(Guid clientId)
        {
            var client = Mapper.Map<ClientResponse>(await clientService.GetClientAsync(clientId));

            return Ok(client);
        }

        /// <summary>
        /// Inclui um novo Cliente
        /// </summary>
        /// <param name="addClientRequest">Client</param>
        /// <returns>Client</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ClientCoreException), 400)]
        [ProducesResponseType(typeof(ClientRequest), 200)]
        public async Task<IActionResult> AddClientAsync([FromBody] ClientRequest addClientRequest)
        {
            ValidationHelper.ThrowValidationExceptionIfNotValid(addClientRequest);

            var client = Mapper.Map<Client>(addClientRequest);

            await clientService.AddClientAsync(client);

            return Ok(client);
        }

        /// <summary>
        /// Atualiza um Cliente
        /// </summary>
        /// <param name="updateClientRequest">Client</param>
        /// <returns>Client</returns>
        [HttpPut]
        [ProducesResponseType(typeof(ClientCoreException), 400)]
        [ProducesResponseType(typeof(ClientRequest), 200)]
        public async Task<IActionResult> UpdateClientAsync([FromBody] ClientRequest updateClientRequest)
        {
            ValidationHelper.ThrowValidationExceptionIfNotValid(updateClientRequest);

            var client = Mapper.Map<Client>(updateClientRequest);

            await clientService.UpdateClientAsync(client);

            return Ok(client);
        }

        /// <summary>
        /// Inclui uma Assinatura para um Cliente
        /// </summary>
        /// <param name="addSubscriptionRequest">Client</param>
        /// <returns>Client</returns>
        [HttpPost("{id}/subscriptions")]
        [ProducesResponseType(typeof(ClientCoreException), 400)]
        [ProducesResponseType(typeof(SubscriptionCoreException), 400)]
        [ProducesResponseType(typeof(ClientRequest), 200)]
        public async Task<IActionResult> AddClientSubscriptionAsync([FromBody] SubscriptionRequest addSubscriptionRequest)
        {
            ValidationHelper.ThrowValidationExceptionIfNotValid(addSubscriptionRequest);

            var subscription = Mapper.Map<Subscription>(addSubscriptionRequest);

            await subscriptionService.AddSubscriptionAsync(subscription);

            return Ok(subscription);
        }

    }
}