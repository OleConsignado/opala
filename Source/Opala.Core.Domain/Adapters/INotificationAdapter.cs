using Opala.Core.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Opala.Core.Domain.Adapters
{
    public interface INotificationAdapter
    {
        /// <summary>
        /// Enviar SMS
        /// </summary>
        /// <param name="number">Número do telefone</param>
        /// <param name="message">Mensagem</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Excessão de sistema</exception>
        Task<NotificationResult> SendAsync(string number, string message);
    }
}
