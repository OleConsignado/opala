using Opala.Core.Domain.Adapters;
using Opala.Core.Domain.Exceptions;
using Opala.Core.Domain.Models;
using Opala.Core.Domain.Repositories;
using Opala.Core.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Opala.Core.Application.Services
{
    public class AssinaturaService :IAssinaturaService
    {
        private readonly IAssinaturaReadOnlyRepository assinaturaReadOnlyRepository;
        private readonly IAssinaturaWriteOnlyRepository assinaturaWriteOnlyRepository;
        private readonly IClienteReadOnlyRepository clienteReadOnlyRepository;
        private readonly INotificacaoAdapter notificacaoAdapter;

        public AssinaturaService(IAssinaturaReadOnlyRepository assinaturaReadOnlyRepository, IAssinaturaWriteOnlyRepository assinaturaWriteOnlyRepository, IClienteReadOnlyRepository clienteReadOnlyRepository, INotificacaoAdapter notificacaoAdapter)
        {
            this.assinaturaReadOnlyRepository = assinaturaReadOnlyRepository ?? throw new ArgumentNullException(nameof(assinaturaReadOnlyRepository));
            this.assinaturaWriteOnlyRepository = assinaturaWriteOnlyRepository ?? throw new ArgumentNullException(nameof(assinaturaWriteOnlyRepository));
            this.clienteReadOnlyRepository = clienteReadOnlyRepository ?? throw new ArgumentNullException(nameof(clienteReadOnlyRepository));
            this.notificacaoAdapter = notificacaoAdapter ?? throw new ArgumentNullException(nameof(notificacaoAdapter));
        }

        public async Task IncluiAssinaturaAsync(Assinatura assinatura)
        {
            if (assinatura == null)
                throw new ArgumentNullException(nameof(assinatura));

            var cliente = await clienteReadOnlyRepository.RetornaClienteSemAssinaturaAsync(assinatura.ClienteId);

            if (cliente == null)
                throw new ClienteCoreException(ClienteCoreError.ClienteNaoEncontrado);

            var temAssinaturaAtiva = cliente.Assinaturas.Any(c => c.Ativa);

            if (temAssinaturaAtiva)
                throw new AssinaturaCoreException().AddError(AssinaturaCoreError.AssinaturaAtiva);

            await assinaturaWriteOnlyRepository.IncluiAssinaturaAsync(assinatura);

            //await notificacaoAdapter.EnviaAsync(cliente.Telefone, "Assinatura incluída com sucesso");
        }

        public async Task<IEnumerable<Assinatura>> ListaAssinaturasClienteAsync(Guid clienteId, int pagina, int totalRegistros)
        {
            var cliente = await clienteReadOnlyRepository.ClienteExisteAsync(clienteId);

            if (!cliente)
                throw new ClienteCoreException(ClienteCoreError.ClienteNaoEncontrado);

            var assinaturas = await assinaturaReadOnlyRepository.RetornaAssinaturasClienteAsync(clienteId, pagina, totalRegistros);

            return assinaturas;
        }

        public async Task<Assinatura> RetornaAssinaturaAsync(Guid id)
        {
            return await assinaturaReadOnlyRepository.RetornaAssinaturaAsync(id);
        }
    }
}
