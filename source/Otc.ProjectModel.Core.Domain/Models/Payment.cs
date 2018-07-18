using System;

namespace Otc.ProjectModel.Core.Domain.Models
{
    public abstract class Payment
    {
        public string Number { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPaid { get; set; }
        public string Payer { get; set; }
        public Address Address { get; set; }

        public Payment()
        {
            Number = Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
