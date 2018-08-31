using System;

namespace Opala.WebApi.Dtos
{
    public class AddCreditCardPaymentPost
    {
        public Guid ClientId { get; set; }
        public Guid SubscriptionId { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPaid { get; set; }
        public string Payer { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string LastTransactionNumber { get; set; }
    }
}
