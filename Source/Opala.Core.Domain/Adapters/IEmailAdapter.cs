using System.Threading.Tasks;

namespace Opala.Core.Domain.Adapters
{
    public interface IEmailAdapter
    {
        /// <summary>
        /// Enviar email
        /// </summary>
        /// <param name="origem">Origem</param>
        /// <param name="destino">Destino</param>
        /// <param name="assunto">Assunto</param>
        /// <param name="mensagem">Mensagem</param>
        /// <returns></returns>
        Task EnviaAsync(string origem, string destino, string assunto, string mensagem);
    }
}
