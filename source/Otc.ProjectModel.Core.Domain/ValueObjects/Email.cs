namespace Otc.ProjectModel.Core.Domain.ValueObjects
{
    public class Email
    {
        public Email(string address)
        {
            Address = address;
        }

        public string Address { get; private set; }
    }
}