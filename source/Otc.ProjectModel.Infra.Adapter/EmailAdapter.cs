using Otc.ProjectModel.Core.Domain.Adapters;
using System;

namespace Otc.ProjectModel.Infra.Adapter
{
    public class EmailAdapter : IEmailAdapter
    {
        public void Send(string to, string email, string subject, string body)
        {
            throw new NotImplementedException();
        }
    }
}
