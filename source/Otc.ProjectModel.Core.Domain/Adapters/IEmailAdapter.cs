namespace Otc.ProjectModel.Core.Domain.Adapters
{
    public interface IEmailAdapter
    {
        void Send(string to, string from, string subject, string body);
    }
}
