using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IPaymentService paymentService;

        public ClientsController(IClientService clientService, ISubscriptionService subscriptionService, IPaymentService paymentService)
        {
            this.clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            this.subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
            this.paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
        }

        /// <summary>
        /// Retorna um Cliente com base no seu identificador
        /// </summary>
        /// <param name="clientId">Identificador</param>
        /// <returns>Client</returns>
        [HttpGet("{clientId}")]
        [ProducesResponseType(typeof(ClientCoreException), 400)]
        [ProducesResponseType(typeof(GetClientResult), 200)]
        public async Task<IActionResult> GetClientAsync(Guid clientId)
        {
            var client = Mapper.Map<GetClientResult>(await clientService.GetClientAsync(clientId));

            if (client == null)
                return NotFound(ClientCoreError.ClientNotFound);

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
        /// Habilita ou desabilita um Cliente
        /// </summary>
        /// <param name="clientId">Identificador</param>
        /// <param name="isActive">True ou False</param>
        /// <returns></returns>
        [HttpPatch("{clientId}/status")]
        [ProducesResponseType(typeof(ClientCoreException), 400)]
        [ProducesResponseType(typeof(AddClientPost), 200)]
        public async Task<IActionResult> EnableDisableClientAsync(Guid clientId, [FromQuery] bool isActive)
        {
            await clientService.EnableDisableClientAsync(clientId, isActive);

            return Ok();
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

            try
            {
                await clientService.UpdateClientAsync(client);

                return Ok(client);
            }
            catch (ClientCoreException ex) when (ex.Errors.Any(c => c.Key == ClientCoreError.ClientNotFound.Key))
            {
                return NotFound(ClientCoreError.ClientNotFound);
            }
        }

        /// <summary>
        /// Remove um Cliente
        /// </summary>
        /// <param name="clientId">Identificador</param>
        /// <returns></returns>
        [HttpDelete("clientId")]
        public async Task<IActionResult> RemoveClientAsync(Guid clientId)
        {
            await clientService.RemoveClientAsync(clientId);

            return Ok();
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

            try
            {
                await subscriptionService.AddSubscriptionAsync(subscription);

                return Ok(subscription);
            }
            catch(ClientCoreException ex) when (ex.Errors.Any(c => c.Key == ClientCoreError.ClientNotFound.Key))
            {
                return NotFound(ClientCoreError.ClientNotFound);
            }
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
            try
            {
                var subscriptions = await subscriptionService.GetClientSubscriptionsAsync(clientId);

                return Ok(subscriptions);
            }
            catch(ClientCoreException ex) when(ex.Errors.Any(c => c.Key == ClientCoreError.ClientNotFound.Key))
            {
                return NotFound(ClientCoreError.ClientNotFound);
            }
        }

        /// <summary>
        /// Inclui um Pagamento do PayPal para a Assinatura
        /// </summary>
        /// <param name="addPayPalPaymentPost">Objeto de pagamento</param>
        /// <returns></returns>
        [HttpPost("{clientId}/subscriptions/{subscriptionId}/payments/paypal")]
        public async Task<IActionResult> AddPayPalPayment([FromBody] AddPayPalPaymentPost addPayPalPaymentPost)
        {
            var payment = Mapper.Map<PayPalPayment>(addPayPalPaymentPost);

            try
            {
                await paymentService.AddPayPalPaymentAsync(payment);

                return Ok(payment);
            }
            catch (ClientCoreException ex) when (ex.Errors.Any(c => c.Key == ClientCoreError.ClientNotFound.Key))
            {
                return NotFound(ClientCoreError.ClientNotFound);
            }
            catch (SubscriptionCoreException ex) when (ex.Errors.Any(c => c.Key == SubscriptionCoreError.SubscriptionNotFound.Key))
            {
                return NotFound(SubscriptionCoreError.SubscriptionNotFound);
            }
        }

        /// <summary>
        /// Inclui um Pagamento do tipo Cartão de Crédito para a Assinatura
        /// </summary>
        /// <param name="addCreditCardPaymentPost"></param>
        /// <returns></returns>
        [HttpPost("{clientId}/subscriptions/{subscriptionId}/payments/creditcard")]
        public async Task<IActionResult> AddCrediCardPayment([FromBody] AddCreditCardPaymentPost addCreditCardPaymentPost)
        {
            var payment = Mapper.Map<CreditCardPayment>(addCreditCardPaymentPost);

            try
            {
                await paymentService.AddCreditCardPaymentAsync(payment);

                return Ok(payment);
            }
            catch (ClientCoreException ex) when (ex.Errors.Any(c => c.Key == ClientCoreError.ClientNotFound.Key))
            {
                return NotFound(ClientCoreError.ClientNotFound);
            }
            catch (SubscriptionCoreException ex) when (ex.Errors.Any(c => c.Key == SubscriptionCoreError.SubscriptionNotFound.Key))
            {
                return NotFound(SubscriptionCoreError.SubscriptionNotFound);
            }

        }

    }
}