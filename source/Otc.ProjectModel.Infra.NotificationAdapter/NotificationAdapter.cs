using Newtonsoft.Json;
using Otc.ProjectModel.Core.Domain.Adapters;
using System;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Infra.NotificationAdapter
{
    public class NotificationAdapter : INotificationAdapter
    {
        private readonly NotificationAdapterConfiguration notificationAdapterConfiguration;

        public NotificationAdapter(NotificationAdapterConfiguration notificationAdapterConfiguration)
        {
            this.notificationAdapterConfiguration = notificationAdapterConfiguration ?? throw new ArgumentNullException(nameof(notificationAdapterConfiguration));
        }

        public async Task SendAsync(string number, string message)
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

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //Todo - Faz alguma coisa com o retorno, caso precise

                    //string data = response.Content.ReadAsStringAsync().Result;
                    //var notificationResponse = JsonConvert.DeserializeObject<NotificationResponse>(data);
                    //return notificationResponse;
                }
                else
                {
                    string data = await response.Content.ReadAsStringAsync();

                    //Todo - modelar como uma excecao de sistema ou de dominio conforme o erro
                }
            }
        }
    }
}
