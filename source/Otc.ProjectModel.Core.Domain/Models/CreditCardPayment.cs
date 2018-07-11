using Otc.ProjectModel.Core.Domain.ValueObjects;
using System;

namespace Otc.ProjectModel.Core.Domain.Models
{
    public class CreditCardPayment : Payment
    {
        public CreditCardPayment(string cardHolderName, string cardNumber, string lastTransactionNumber, DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, string payer, Address address, Email email) : base(paidDate, expireDate, total, totalPaid, payer, address, email)
        {
            CardHolderName = cardHolderName;
            CardNumber = cardNumber;
            LastTransactionNumber = lastTransactionNumber;
        }

        public string CardHolderName { get; private set; }
        public string CardNumber { get; private set; }
        public string LastTransactionNumber { get; private set; }
    }
}
