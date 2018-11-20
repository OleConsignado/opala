using System.Threading.Tasks;
using Refit;

namespace Opala.Infra.NotificationAdapter.Clients
{
    public interface INotificacaoClient
    {
        [Post("/api/Sms/EnviarSms")]
        Task<NotificacaoResponse> EnviaNotificacaoAsync([Body]NotificacaoRequest request);
    }
}
