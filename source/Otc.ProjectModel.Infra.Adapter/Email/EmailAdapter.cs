using Otc.ProjectModel.Core.Domain.Adapters;

namespace Otc.ProjectModel.Infra.Adapter.Email
{
    public class EmailAdapter : IEmailAdapter
    {
        public void Send(string to, string from, string subject, string body)
        {
            // Implementar um envio de email
        }
    }
}
