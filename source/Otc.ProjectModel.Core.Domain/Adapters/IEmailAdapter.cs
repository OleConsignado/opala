namespace Otc.ProjectModel.Core.Domain.Adapters
{
    public interface IEmailAdapter
    {
        void Send(string to, string email, string subject, string body);
    }
}
