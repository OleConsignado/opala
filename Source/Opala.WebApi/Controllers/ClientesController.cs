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
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService clienteService;
        private readonly IAssinaturaService assinaturaService;
        private readonly IPagamentoService pagamentoService;

        public ClientesController(IClienteService clienteService, IAssinaturaService assinaturaService, IPagamentoService pagamentoService)
        {
            this.clienteService = clienteService ?? throw new ArgumentNullException(nameof(clienteService));
            this.assinaturaService = assinaturaService ?? throw new ArgumentNullException(nameof(assinaturaService));
            this.pagamentoService = pagamentoService ?? throw new ArgumentNullException(nameof(pagamentoService));
        }

        /// <summary>
        /// Retorna um Cliente com base no seu identificador
        /// </summary>
        /// <param name="clienteId">Identificador</param>
        /// <returns>Client</returns>
        [HttpGet("{clienteId}")]
        [ProducesResponseType(typeof(ClienteCoreException), 400)]
        [ProducesResponseType(typeof(RetornaClienteGet), 200)]
        public async Task<IActionResult> RetornaClienteAsync(Guid clienteId)
        {
            var cliente = Mapper.Map<RetornaClienteGet>(await clienteService.RetornaClienteAsync(clienteId));

            if (cliente == null)
                return NotFound(ClienteCoreError.ClienteNaoEncontrado);

            return Ok(cliente);
        }

        /// <summary>
        /// Inclui um novo Cliente
        /// </summary>
        /// <param name="incluiClientePost">Cliente</param>
        /// <returns>Client</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ClienteCoreException), 400)]
        [ProducesResponseType(typeof(IncluiClientePost), 200)]
        public async Task<IActionResult> IncluiClienteAsync([FromBody] IncluiClientePost incluiClientePost)
        {
            var cliente = Mapper.Map<Cliente>(incluiClientePost);

            await clienteService.IncluiClienteAsync(cliente);

            return Ok(cliente);
        }

        /// <summary>
        /// Habilita ou desabilita um Cliente
        /// </summary>
        /// <param name="clienteId">Identificador</param>
        /// <param name="ativo">True ou False</param>
        /// <returns></returns>
        [HttpPatch("{clienteId}/status")]
        [ProducesResponseType(typeof(ClienteCoreException), 400)]
        public async Task<IActionResult> AtivaDesativaClienteAsync(Guid clienteId, [FromQuery] bool ativo)
        {
            await clienteService.AtivaDesativaClienteAsync(clienteId, ativo);

            return Ok();
        }

        /// <summary>
        /// Atualiza um Cliente
        /// </summary>
        /// <param name="clienteId"></param>
        /// <param name="atualizaClientePut">Cliente</param>
        /// <returns>Client</returns>
        [HttpPut("{clienteId}")]
        [ProducesResponseType(typeof(ClienteCoreException), 400)]
        [ProducesResponseType(typeof(AtualizaClientePut), 200)]
        public async Task<IActionResult> AtualizaClienteAsync(Guid clienteId, [FromBody] AtualizaClientePut atualizaClientePut)
        {
            var cliente = Mapper.Map<Cliente>(atualizaClientePut);
            cliente.Id = clienteId;

            try
            {
                await clienteService.AtualizaClienteAsync(cliente);

                return Ok(cliente);
            }
            catch (ClienteCoreException ex) when (ex.Errors.Any(c => c.Key == ClienteCoreError.ClienteNaoEncontrado.Key))
            {
                return NotFound(ClienteCoreError.ClienteNaoEncontrado);
            }
        }

        /// <summary>
        /// Remove um Cliente
        /// </summary>
        /// <param name="clienteId">Identificador</param>
        /// <returns></returns>
        [HttpDelete("clienteId")]
        [ProducesResponseType(typeof(ClienteCoreException), 400)]
        public async Task<IActionResult> ExcluiClienteAsync(Guid clienteId)
        {
            await clienteService.ExcluiClienteAsync(clienteId);

            return Ok();
        }

        /// <summary>
        /// Inclui uma Assinatura para um Cliente
        /// </summary>
        /// <param name="incluiAssinaturaClientePost">Client</param>
        /// <returns>Client</returns>
        [HttpPost("{clienteId}/assinaturas")]
        [ProducesResponseType(typeof(ClienteCoreException), 400)]
        [ProducesResponseType(typeof(AssinaturaCoreException), 400)]
        [ProducesResponseType(typeof(IncluiAssinaturaClientePost), 200)]
        public async Task<IActionResult> IncluiAssinaturaClienteAsync([FromBody] IncluiAssinaturaClientePost incluiAssinaturaClientePost)
        {
            var assinaturas = Mapper.Map<Assinatura>(incluiAssinaturaClientePost);

            try
            {
                await assinaturaService.IncluiAssinaturaAsync(assinaturas);

                return Ok(assinaturas);
            }
            catch(ClienteCoreException ex) when (ex.Errors.Any(c => c.Key == ClienteCoreError.ClienteNaoEncontrado.Key))
            {
                return NotFound(ClienteCoreError.ClienteNaoEncontrado);
            }
        }

        /// <summary>
        /// Listas as Assinaturas de um Cliente
        /// </summary>
        /// <param name="clienteId">Identificador do Cliente</param>
        /// <param name="pageOptions">Parâmetros de pesquisa</param>
        /// <returns>Client</returns>
        [HttpGet("{clienteId}/assinaturas")]
        [ProducesResponseType(typeof(AssinaturaCoreException), 400)]
        [ProducesResponseType(typeof(IEnumerable<Assinatura>), 200)]
        public async Task<IActionResult> RetornaAssinaturasClienteAsync(Guid clienteId, [FromQuery] PageOptions pageOptions)
        {
            ValidationHelper.ThrowValidationExceptionIfNotValid(pageOptions);

            try
            {
                var assinaturas = await assinaturaService.ListaAssinaturasClienteAsync(clienteId, pageOptions.Page.Value, pageOptions.Count.Value);

                return Ok(assinaturas);
            }
            catch(ClienteCoreException ex) when(ex.Errors.Any(c => c.Key == ClienteCoreError.ClienteNaoEncontrado.Key))
            {
                return NotFound(ClienteCoreError.ClienteNaoEncontrado);
            }
        }

        /// <summary>
        /// Inclui um Pagamento do PayPal para a Assinatura
        /// </summary>
        /// <param name="incluiPagamentoPayPalPost">Objeto de pagamento</param>
        /// <returns></returns>
        [HttpPost("{clienteId}/assinaturas/{assinaturaId}/pagamentos/paypal")]
        [ProducesResponseType(typeof(ClienteCoreException), 400)]
        [ProducesResponseType(typeof(AssinaturaCoreException), 400)]
        [ProducesResponseType(typeof(IncluiPagamentoPayPalPost), 200)]
        public async Task<IActionResult> IncluiPagamentoPayPalAsync([FromBody] IncluiPagamentoPayPalPost incluiPagamentoPayPalPost)
        {
            var pagamento = Mapper.Map<PayPal>(incluiPagamentoPayPalPost);

            try
            {
                await pagamentoService.IncluiPagamentoPayPalAsync(pagamento);

                return Ok(pagamento);
            }
            catch (ClienteCoreException ex) when (ex.Errors.Any(c => c.Key == ClienteCoreError.ClienteNaoEncontrado.Key))
            {
                return NotFound(ClienteCoreError.ClienteNaoEncontrado);
            }
            catch (AssinaturaCoreException ex) when (ex.Errors.Any(c => c.Key == AssinaturaCoreError.AssinaturaNaoEncontrada.Key))
            {
                return NotFound(AssinaturaCoreError.AssinaturaNaoEncontrada);
            }
        }

        /// <summary>
        /// Inclui um Pagamento do tipo Cartão de Crédito para a Assinatura
        /// </summary>
        /// <param name="incluiPagamentoCartaoCreditoPost"></param>
        /// <returns></returns>
        [HttpPost("{clienteId}/assinaturas/{assinaturaId}/pagamentos/cartaocredito")]
        [ProducesResponseType(typeof(ClienteCoreException), 400)]
        [ProducesResponseType(typeof(AssinaturaCoreException), 400)]
        [ProducesResponseType(typeof(IncluiPagamentoCartaoCreditoPost), 200)]
        public async Task<IActionResult> IncluiPagamentoCartaoCreditoAsync([FromBody] IncluiPagamentoCartaoCreditoPost incluiPagamentoCartaoCreditoPost)
        {
            var pagamento = Mapper.Map<CartaoCredito>(incluiPagamentoCartaoCreditoPost);

            try
            {
                await pagamentoService.IncluiPagamentoCartaoCreditoAsync(pagamento);

                return Ok(pagamento);
            }
            catch (ClienteCoreException ex) when (ex.Errors.Any(c => c.Key == ClienteCoreError.ClienteNaoEncontrado.Key))
            {
                return NotFound(ClienteCoreError.ClienteNaoEncontrado);
            }
            catch (AssinaturaCoreException ex) when (ex.Errors.Any(c => c.Key == AssinaturaCoreError.AssinaturaNaoEncontrada.Key))
            {
                return NotFound(AssinaturaCoreError.AssinaturaNaoEncontrada);
            }

        }

        /// <summary>
        /// Retorna um Pagamento
        /// </summary>
        /// <param name="clienteId">Identificador do Cliente</param>
        /// <param name="assinaturaId">Identificador da Assinatura</param>
        /// <param name="pagamentoId">Identificador do Pagamento</param>
        /// <param name="formaPagamento">Tipo de Pagamento (1 - paypal | 2 - creditcard)</param>
        /// <returns></returns>
        [HttpGet("{clienteId}/assinaturas/{assinaturaId}/pagamentos/{pagamentoId}/")]
        [ProducesResponseType(typeof(ClienteCoreException), 400)]
        [ProducesResponseType(typeof(AssinaturaCoreException), 400)]
        [ProducesResponseType(typeof(Pagamento), 200)]
        public async Task<IActionResult> RetornaPagamentoAsync(Guid clienteId, Guid assinaturaId, Guid pagamentoId, [FromQuery] FormaPagamento formaPagamento)
        {
            Pagamento pagamento;

            switch (formaPagamento)
            {
                case FormaPagamento.PayPal:
                    pagamento = await pagamentoService.RetornaPagamentoPayPalAsync(clienteId, assinaturaId, pagamentoId);
                    break;
                case FormaPagamento.CartaoCredito:
                    pagamento = await pagamentoService.RetornaPagamentoCartaoCreditoAsync(clienteId, assinaturaId, pagamentoId);
                    break;
                default:
                    pagamento = null;
                    break;
            }

            return Ok(pagamento);
        }

        /// <summary>
        /// Retorna ums lista Pagamentos
        /// </summary>
        /// <param name="clienteId">Identificador do Cliente</param>
        /// <param name="assinaturaId">Identificador da Assinatura</param>
        /// <returns></returns>
        [HttpGet("{clienteId}/assinaturas/{assinaturaId}/pagamentos/")]
        [ProducesResponseType(typeof(ClienteCoreException), 400)]
        [ProducesResponseType(typeof(AssinaturaCoreException), 400)]
        [ProducesResponseType(typeof(IEnumerable<Pagamento>), 200)]
        public async Task<IActionResult> ListaPagamentosAsync(Guid clienteId, Guid assinaturaId)
        {
             var pagamentos = await pagamentoService.RetornaPagamentosAssinaturaAsync(clienteId, assinaturaId);

            return Ok(pagamentos);
        }
    }
}