using Otc.ProjectModel.Core.Domain.ValueObjects;
using System;

namespace Otc.ProjectModel.Core.Domain.Models
{
    public class Payment : Entity
    {
        public string Number { get; private set; }
        public DateTime PaidDate { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public decimal Total { get; private set; }
        public decimal TotalPaid { get; private set; }
        public string Payer { get; private set; }
        public Address Address { get; private set; }
        public Email Email { get; private set; }

        public Payment(DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, string payer, Address address, Email email)
        {
            Number = Guid.NewGuid().ToString().Replace("-", "");
            PaidDate = paidDate;
            ExpireDate = expireDate;
            Total = total;
            TotalPaid = totalPaid;
            Payer = payer;
            Address = address;
            Email = email;
        }
    }
}
