using System.Threading.Tasks;
using Refit;

namespace Opala.Infra.NotificationAdapter.Clients
{
    public interface INotificationClient
    {
        [Post("/api/Sms/EnviarSms")]
        Task<NotificationResponse> SendNotificationAsync([Body]NotificationRequest request);
    }
}
