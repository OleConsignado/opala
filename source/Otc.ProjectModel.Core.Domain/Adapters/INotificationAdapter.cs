namespace Otc.ProjectModel.Core.Domain.Adapters
{
    public interface INotificationAdapter
    {
        void Send(string number, string message);
    }
}
