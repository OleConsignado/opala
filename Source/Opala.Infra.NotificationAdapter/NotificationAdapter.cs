using Newtonsoft.Json;
using Opala.Core.Domain.Adapters;
using System;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Opala.Infra.NotificationAdapter.Exceptions;
using Opala.Core.Domain.Models;

namespace Opala.Infra.NotificationAdapter
{
    public class NotificationAdapter : INotificationAdapter
    {
        private readonly NotificationAdapterConfiguration notificationAdapterConfiguration;

        public NotificationAdapter(NotificationAdapterConfiguration notificationAdapterConfiguration)
        {
            this.notificationAdapterConfiguration = notificationAdapterConfiguration ?? throw new ArgumentNullException(nameof(notificationAdapterConfiguration));
        }

        public async Task<NotificationResult> SendAsync(string number, string message)
        {
            if (number == null)
                throw new ArgumentNullException(nameof(number));

            if (message == null)
                throw new ArgumentNullException(nameof(message));

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(notificationAdapterConfiguration.NotificationUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var notificationRequest = new NotificationRequest
                {
                    Number = number,
                    Message = message
                };

                var postedData = JsonConvert.SerializeObject(notificationRequest);
                var content = new StringContent(postedData, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(@"api/Sms/EnviarSms", content);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    var notificationResponse = JsonConvert.DeserializeObject<NotificationResponse>(data);

                    return new NotificationResult
                    {
                        EnvioId = notificationResponse.Id,
                        Response = notificationResponse.Response,
                        Status = notificationResponse.Status
                    };
                }
                else
                {
                    string data = await response.Content.ReadAsStringAsync();

                    throw new NotificationAdapterException(data, response.StatusCode);
                }
            }
        }
    }
}
