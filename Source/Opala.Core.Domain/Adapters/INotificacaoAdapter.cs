using Opala.Core.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Opala.Core.Domain.Adapters
{
    public interface INotificacaoAdapter
    {
        /// <summary>
        /// Enviar SMS
        /// </summary>
        /// <param name="telefone">Número do telefone</param>
        /// <param name="mensagem">Mensagem</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Excessão de sistema</exception>
        Task<NotificationResult> EnviaAsync(string telefone, string mensagem);
    }
}
