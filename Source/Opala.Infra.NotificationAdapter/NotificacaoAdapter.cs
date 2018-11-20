using Opala.Core.Domain.Adapters;
using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Opala.Core.Domain.Exceptions;
using Opala.Core.Domain.Models;
using Opala.Infra.NotificationAdapter.Clients;
using Opala.Infra.NotificationAdapter.Exceptions;
using Refit;

namespace Opala.Infra.NotificationAdapter
{
    public class NotificacaoAdapter : INotificacaoAdapter
    {
        private readonly INotificacaoClient notificacaoClient;

        public NotificacaoAdapter(INotificacaoClient notificacaoClient)
        {
            this.notificacaoClient = notificacaoClient ?? throw new ArgumentNullException(nameof(notificacaoClient));
        }

        public async Task<NotificationResult> EnviaAsync(string number, string message)
        {
            NotificacaoRequest request = new NotificacaoRequest
            {
                Numero = number,
                Mensagem = message
            };

            try
            {
                var response = await notificacaoClient.EnviaNotificacaoAsync(request);

                var result = Mapper.Map<NotificacaoResponse, NotificationResult>(response);

                return result;
            }
            catch (ApiException e) when (e.StatusCode == HttpStatusCode.BadRequest)
            {
                var e400 = e.GetContentAs<CoreExceptionDto>();
                if (e400.TypeName == "NotificacaoCoreException")
                    throw new NotificacaoCoreException();

                throw;
            }
        }
    }
}
