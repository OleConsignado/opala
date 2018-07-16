using Newtonsoft.Json;
using Otc.ProjectModel.Core.Domain.Adapters;
using System;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Otc.ProjectModel.Infra.Adapter.Notification
{
    public class NotificationAdapter : INotificationAdapter
    {
        private readonly NotificationAdapterConfiguration notificationAdapterConfiguration;

        public NotificationAdapter(NotificationAdapterConfiguration notificationAdapterConfiguration)
        {
            this.notificationAdapterConfiguration = notificationAdapterConfiguration;
        }

        public void Send(string number, string message)
        {
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
                var response = client.PostAsync(@"api/Sms/EnviarSms", content).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //Todo - Faz alguma coisa com o retorno, caso precise

                    //string data = response.Content.ReadAsStringAsync().Result;
                    //var notificationResponse = JsonConvert.DeserializeObject<NotificationResponse>(data);
                    //return notificationResponse;
                }
                else
                {
                    string data = response.Content.ReadAsStringAsync().Result;

                    throw new Exception(data);
                }
            }
        }
    }
}
