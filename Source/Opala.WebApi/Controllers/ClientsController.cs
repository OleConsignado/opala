using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Opala.Core.Domain.Exceptions;
using Opala.Core.Domain.Models;
using Opala.Core.Domain.Services;
using Opala.WebApi.Dtos;
using Otc.Validations.Helpers;

namespace Opala.WebApi.Controllers
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
        [ProducesResponseType(typeof(ClientCoreException), 404)]
        [ProducesResponseType(typeof(GetClientResult), 200)]
        public async Task<IActionResult> GetClientAsync(Guid clientId)
        {
            var clientResult = await clientService.GetClientAsync(clientId);

            if (clientResult == null)
                return NotFound(ClientCoreError.ClientNotFound);

            var client =  Mapper.Map<GetClientResult>(clientResult);

            return Ok(clientResult);
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
        [ProducesResponseType(typeof(ClientCoreException), 400)]
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
        /// <param name="pageOptions">Parâmetros de pesquisa</param>
        /// <returns>Client</returns>
        [HttpGet("{clientId}/subscriptions")]
        [ProducesResponseType(typeof(SubscriptionCoreException), 400)]
        [ProducesResponseType(typeof(IEnumerable<Subscription>), 200)]
        public async Task<IActionResult> GetClientSubscriptionsAsync(Guid clientId, [FromQuery] PageOptions pageOptions)
        {
            ValidationHelper.ThrowValidationExceptionIfNotValid(pageOptions);

            try
            {
                var subscriptions = await subscriptionService.GetClientSubscriptionsAsync(clientId, pageOptions.Page.Value, pageOptions.Count.Value);

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
        [ProducesResponseType(typeof(ClientCoreException), 400)]
        [ProducesResponseType(typeof(SubscriptionCoreException), 400)]
        [ProducesResponseType(typeof(AddPayPalPaymentPost), 200)]
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
        [ProducesResponseType(typeof(ClientCoreException), 400)]
        [ProducesResponseType(typeof(SubscriptionCoreException), 400)]
        [ProducesResponseType(typeof(AddCreditCardPaymentPost), 200)]
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

        /// <summary>
        /// Retorna um Pagamento
        /// </summary>
        /// <param name="clientId">Identificador do Cliente</param>
        /// <param name="subscriptionId">Identificador da Assinatura</param>
        /// <param name="paymentId">Identificador do Pagamento</param>
        /// <param name="paymentType">Tipo de Pagamento (1 - paypal | 2 - creditcard)</param>
        /// <returns></returns>
        [HttpGet("{clientId}/subscriptions/{subscriptionId}/payments/{paymentId}/")]
        [ProducesResponseType(typeof(ClientCoreException), 400)]
        [ProducesResponseType(typeof(SubscriptionCoreException), 400)]
        [ProducesResponseType(typeof(Payment), 200)]
        public async Task<IActionResult> GetPayment(Guid clientId, Guid subscriptionId, Guid paymentId, [FromQuery] PaymentType paymentType)
        {
            Payment payment;

            switch (paymentType)
            {
                case PaymentType.PayPal:
                    payment = await paymentService.GetPayPalPaymentAsync(clientId, subscriptionId, paymentId) as PayPalPayment;
                    break;
                case PaymentType.CreditCard:
                    payment = await paymentService.GetCreditCardPaymentAsync(clientId, subscriptionId, paymentId) as CreditCardPayment;
                    break;
                default:
                    payment = null;
                    break;
            }

            return Ok(payment);
        }

        /// <summary>
        /// Retorna ums lista Pagamentos
        /// </summary>
        /// <param name="clientId">Identificador do Cliente</param>
        /// <param name="subscriptionId">Identificador da Assinatura</param>
        /// <returns></returns>
        [HttpGet("{clientId}/subscriptions/{subscriptionId}/payments/")]
        [ProducesResponseType(typeof(ClientCoreException), 400)]
        [ProducesResponseType(typeof(SubscriptionCoreException), 400)]
        [ProducesResponseType(typeof(IEnumerable<Payment>), 200)]
        public async Task<IActionResult> GetPayments(Guid clientId, Guid subscriptionId)
        {
             var payments = await paymentService.GetPaymentsFromSubscriptionAsync(clientId, subscriptionId);

            return Ok(payments);
        }
    }
}