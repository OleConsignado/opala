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
    public class NotificationAdapter : INotificationAdapter
    {
        private readonly INotificationClient notificationClient;

        public NotificationAdapter(INotificationClient notificationClient)
        {
            this.notificationClient = notificationClient ?? throw new ArgumentNullException(nameof(notificationClient));
        }

        public async Task<NotificationResult> SendAsync(string number, string message)
        {
            NotificationRequest request = new NotificationRequest
            {
                Number = number,
                Message = message
            };

            try
            {
                var response = await notificationClient.SendNotificationAsync(request);

                var result = Mapper.Map<NotificationResponse, NotificationResult>(response);

                return result;
            }
            catch (ApiException e) when (e.StatusCode == HttpStatusCode.BadRequest)
            {
                var e400 = e.GetContentAs<CoreExceptionDto>();
                if (e400.TypeName == "NotificationCoreException")
                    throw new NotificationCoreException();

                throw;
            }
        }
    }
}
