using Otc.ProjectModel.Core.Domain.Adapters;
using Otc.ProjectModel.Core.Domain.Exceptions;

namespace Otc.ProjectModel.Core.Test.Mock
{
    public class FakeEmailAdapter : IEmailAdapter
    {
        public bool Send(string to, string from, string subject, string body)
        {
            if (string.IsNullOrEmpty(to) || string.IsNullOrEmpty(from))
                throw new EmailCoreException().AddError(EmailCoreError.EmailInvalidError);

            // Envia email...

            return true;
        }
    }
}
