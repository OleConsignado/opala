using Otc.ProjectModel.Core.Domain.ValueObjects;
using System;

namespace Otc.ProjectModel.Core.Domain.Models
{
    public class PayPalPayment : Payment
    {
        public PayPalPayment(string transactionCode, DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, string payer, Address address) : base(paidDate, expireDate, total, totalPaid, payer, address)
        {
            TransactionCode = transactionCode;
        }

        public string TransactionCode { get; private set; }
    }
}
