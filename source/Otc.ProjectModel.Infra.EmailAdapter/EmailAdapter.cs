using Otc.ProjectModel.Core.Domain.Adapters;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Infra.EmailAdapter
{
    public class EmailAdapter : IEmailAdapter
    {
        public async Task SendAsync(string to, string from, string subject, string body)
        {
            // Implementar um envio de email
        }
    }
}
