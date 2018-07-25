using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Otc.ProjectModel.Core.Domain.Exceptions;
using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Services;
using Otc.ProjectModel.WebApi.Dtos;

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
        [ProducesResponseType(typeof(ClientGet), 200)]
        public async Task<IActionResult> GetClientAsync(Guid clientId)
        {
            var client = Mapper.Map<ClientGet>(await clientService.GetClientAsync(clientId));

            return Ok(client);
        }

        /// <summary>
        /// Inclui um novo Cliente
        /// </summary>
        /// <param name="addClientRequest">Cliente</param>
        /// <returns>Client</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ClientCoreException), 400)]
        [ProducesResponseType(typeof(AddClientPost), 200)]
        public async Task<IActionResult> AddClientAsync([FromBody] AddClientPost addClientRequest)
        {
            var client = Mapper.Map<Client>(addClientRequest);

            await clientService.AddClientAsync(client);

            return Ok(client);
        }

        /// <summary>
        /// Atualiza um Cliente
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="updateClientRequest">Cliente</param>
        /// <returns>Client</returns>
        [HttpPut("{clientId}")]
        [ProducesResponseType(typeof(ClientCoreException), 400)]
        [ProducesResponseType(typeof(UpdateClientPut), 200)]
        public async Task<IActionResult> UpdateClientAsync(Guid clientId, [FromBody] UpdateClientPut updateClientRequest)
        {
            var client = Mapper.Map<Client>(updateClientRequest);
            client.Id = clientId;

            await clientService.UpdateClientAsync(client);

            return Ok(client);
        }

        /// <summary>
        /// Inclui uma Assinatura para um Cliente
        /// </summary>
        /// <param name="addSubscriptionRequest">Client</param>
        /// <returns>Client</returns>
        [HttpPost("{clientId}/subscriptions")]
        [ProducesResponseType(typeof(ClientCoreException), 400)]
        [ProducesResponseType(typeof(SubscriptionCoreException), 400)]
        [ProducesResponseType(typeof(AddClientSubscriptionPost), 200)]
        public async Task<IActionResult> AddClientSubscriptionAsync([FromBody] AddClientSubscriptionPost addSubscriptionRequest)
        {
            var subscription = Mapper.Map<Subscription>(addSubscriptionRequest);

            await subscriptionService.AddSubscriptionAsync(subscription);

            return Ok(subscription);
        }

        /// <summary>
        /// Listas as Assinaturas de um Cliente
        /// </summary>
        /// <param name="clientId">Identificador do Cliente</param>
        /// <returns>Client</returns>
        [HttpGet("{clientId}/subscriptions")]
        [ProducesResponseType(typeof(SubscriptionCoreException), 400)]
        [ProducesResponseType(typeof(IEnumerable<Subscription>), 200)]
        public async Task<IActionResult> GetClientSubscriptionsAsync(Guid clientId)
        {
           var subscriptions = await subscriptionService.GetClientSubscriptionsAsync(clientId);

            return Ok(subscriptions);
        }
    }
}