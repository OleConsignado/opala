using System;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Core.Domain.Adapters
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
        Task SendAsync(string number, string message);
    }
}
