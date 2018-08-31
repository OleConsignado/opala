using System.Threading.Tasks;

namespace Opala.Core.Domain.Adapters
{
    public interface IEmailAdapter
    {
        /// <summary>
        /// Enviar email
        /// </summary>
        /// <param name="to">Origem</param>
        /// <param name="from">Destino</param>
        /// <param name="subject">Assunto</param>
        /// <param name="body">Mensagem</param>
        /// <returns></returns>
        Task SendAsync(string to, string from, string subject, string body);
    }
}
