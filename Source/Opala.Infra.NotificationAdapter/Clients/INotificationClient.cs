using System.Threading.Tasks;
using Refit;

namespace Opala.Infra.NotificationAdapter.Clients
{
    public interface INotificationClient
    {
        [Post("/api/Sms/EnviarSms")]
        Task<NotificationResponse> SendNotification([Body]NotificationRequest request);
    }
}
