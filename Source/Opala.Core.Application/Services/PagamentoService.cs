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
    public class PagamentoService : IPagamentoService
    {
        private readonly IClienteService clienteService;
        private readonly IAssinaturaService assinaturaService;
        private readonly IPagamentoWriteOnlyRepository pagamentoWriteOnlyRepository;
        private readonly IPagamentoReadOnlyRepository pagamentoReadOnlyRepository;

        public PagamentoService(IClienteService clienteService, IAssinaturaService assinaturaService, IPagamentoWriteOnlyRepository pagamentoWriteOnlyRepository, IPagamentoReadOnlyRepository pagamentoReadOnlyRepository)
        {
            this.clienteService = clienteService ?? throw new System.ArgumentNullException(nameof(clienteService));
            this.assinaturaService = assinaturaService ?? throw new System.ArgumentNullException(nameof(assinaturaService));
            this.pagamentoWriteOnlyRepository = pagamentoWriteOnlyRepository ?? throw new System.ArgumentNullException(nameof(pagamentoWriteOnlyRepository));
            this.pagamentoReadOnlyRepository = pagamentoReadOnlyRepository ?? throw new ArgumentNullException(nameof(pagamentoReadOnlyRepository));
        }

        private async Task ValidaPagamento(Pagamento pagamento)
        {
            if (pagamento == null)
                throw new ArgumentNullException(nameof(pagamento));

            await VerificaClienteExiste(pagamento.ClienteId);

            await VerificaAssinaturaExiste(pagamento.AssinaturaId);
        }

        private async Task VerificaAssinaturaExiste(Guid assinaturaId)
        {
            var assinatura = await assinaturaService.RetornaAssinaturaAsync(assinaturaId);

            if (assinatura == null)
                throw new AssinaturaCoreException(AssinaturaCoreError.AssinaturaNaoEncontrada);
        }

        private async Task VerificaClienteExiste(Guid clienteId)
        {
            var cliente = await clienteService.ClienteExisteAsync(clienteId);

            if (!cliente)
                throw new ClienteCoreException(ClienteCoreError.ClienteNaoEncontrado);
        }

        public async Task IncluiPagamentoCartaoCreditoAsync(CartaoCredito pagamentoCartaoCredito)
        {
            await ValidaPagamento(pagamentoCartaoCredito);

            ValidationHelper.ThrowValidationExceptionIfNotValid(pagamentoCartaoCredito);

            await pagamentoWriteOnlyRepository.IncluiPagamentoCartaoCreditoAsync(pagamentoCartaoCredito);
        }

        public async Task IncluiPagamentoPayPalAsync(PayPal pagamentoPayPal)
        {
            await ValidaPagamento(pagamentoPayPal);

            ValidationHelper.ThrowValidationExceptionIfNotValid(pagamentoPayPal);

            await pagamentoWriteOnlyRepository.IncluiPagamentoPayPalAsync(pagamentoPayPal);
        }

        public async Task<PayPal> RetornaPagamentoPayPalAsync(Guid clienteId, Guid assinaturaId, Guid pagamentoId)
        {
            await VerificaClienteExiste(clienteId);

            await VerificaAssinaturaExiste(assinaturaId);


            if (!(await pagamentoReadOnlyRepository.RetornaPagamentoAsync(clienteId, assinaturaId, pagamentoId) is PayPal pagamento))
                throw new PagamentoCoreException(PagamentoCoreError.PagamentoNaoEncontrado);

            return pagamento;
        }

        public async Task<CartaoCredito> RetornaPagamentoCartaoCreditoAsync(Guid clienteId, Guid assinaturaId, Guid pagamentoId)
        {
            await VerificaClienteExiste(clienteId);

            await VerificaAssinaturaExiste(assinaturaId);


            if (!(await pagamentoReadOnlyRepository.RetornaPagamentoAsync(clienteId, assinaturaId, pagamentoId) is CartaoCredito pagamento))
                throw new PagamentoCoreException(PagamentoCoreError.PagamentoNaoEncontrado);

            return pagamento;
        }

        public async Task<IEnumerable<Pagamento>> RetornaPagamentosAssinaturaAsync(Guid clienteId, Guid assinaturaId)
        {
            await VerificaClienteExiste(clienteId);
            await VerificaAssinaturaExiste(assinaturaId);

            var pagamentos = await pagamentoReadOnlyRepository.RetornaPagamentosAssinaturaAsync(clienteId, assinaturaId);

            return pagamentos;
        }
    }
}
