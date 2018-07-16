namespace Otc.ProjectModel.Core.Domain.Adapters
{
    public interface IEmailAdapter
    {
        bool Send(string to, string from, string subject, string body);
    }
}
