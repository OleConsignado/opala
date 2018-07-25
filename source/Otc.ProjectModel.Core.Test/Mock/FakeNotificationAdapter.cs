using Otc.ProjectModel.Core.Domain.Adapters;
using System;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Core.Test.Mock
{
    public class FakeNotificationAdapter : INotificationAdapter
    {
        public Task SendAsync(string number, string message)
        {
            throw new NotImplementedException();
        }
    }
}
