using System;

namespace Otc.ProjectModel.WebApi.Dtos
{
    public class AddPayPalPaymentPost
    {
        public Guid ClientId { get; set; }
        public Guid SubscriptionId { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPaid { get; set; }
        public string Payer { get; set; }
        public string TransactionCode { get; set; }
    }
}
