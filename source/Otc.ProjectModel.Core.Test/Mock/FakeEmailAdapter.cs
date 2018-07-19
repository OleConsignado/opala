using Otc.ProjectModel.Core.Domain.Adapters;
using Otc.ProjectModel.Core.Domain.Exceptions;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Core.Test.Mock
{
    public class FakeEmailAdapter : IEmailAdapter
    {
        public async Task SendAsync(string to, string from, string subject, string body)
        {
            if (string.IsNullOrEmpty(to) || string.IsNullOrEmpty(from))
                throw new EmailCoreException().AddError(EmailCoreError.EmailInvalidError);

            // Envia email...
        }
    }
}
