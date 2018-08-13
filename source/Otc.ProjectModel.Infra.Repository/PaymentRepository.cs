using Dapper;
using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Infra.Repository
{
    public class PaymentRepository : IPaymentWriteOnlyRepository, IPaymentReadOnlyRepository
    {
        private readonly IDbConnection dbConnection;

        static PaymentRepository() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public PaymentRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public async Task AddCreditCardPaymentAsync(CreditCardPayment payment)
        {
            if (payment == null)
                throw new ArgumentNullException(nameof(payment));

            var paymentParams = new DynamicParameters();
            paymentParams.Add("Id", payment.Id);
            paymentParams.Add("SubscriptionId", payment.SubscriptionId);
            paymentParams.Add("PaidDate", payment.PaidDate, DbType.DateTimeOffset);
            paymentParams.Add("ExpireDate", payment.ExpireDate, DbType.DateTimeOffset);
            paymentParams.Add("Total", payment.Total, DbType.Decimal);
            paymentParams.Add("TotalPaid", payment.TotalPaid, DbType.Decimal);
            paymentParams.Add("Payer", payment.Payer);
            paymentParams.Add("Discriminator", payment.GetType().Name);
            paymentParams.Add("CardHolderName", payment.CardHolderName);
            paymentParams.Add("CardNumber", payment.CardNumber);
            paymentParams.Add("LastTransactionNumber", payment.LastTransactionNumber);

            var queryClient = @"INSERT INTO Payment (Id, SubscriptionId, PaidDate, ExpireDate, Total, TotalPaid, Payer, Discriminator, CardHolderName, CardNumber, LastTransactionNumber) 
                                VALUES (@Id, @SubscriptionId, @PaidDate, @ExpireDate, @Total, @TotalPaid, @Payer, @Discriminator, @CardHolderName, @CardNumber, @LastTransactionNumber)";

            await dbConnection.ExecuteAsync(queryClient, paymentParams);
        }

        public async Task AddPayPalPaymentAsync(PayPalPayment payment)
        {
            if (payment == null)
                throw new ArgumentNullException(nameof(payment));

            var paymentParams = new DynamicParameters();
            paymentParams.Add("Id", payment.Id);
            paymentParams.Add("SubscriptionId", payment.SubscriptionId);
            paymentParams.Add("PaidDate", payment.PaidDate, DbType.DateTimeOffset);
            paymentParams.Add("ExpireDate", payment.ExpireDate, DbType.DateTimeOffset);
            paymentParams.Add("Total", payment.Total, DbType.Decimal);
            paymentParams.Add("TotalPaid", payment.TotalPaid, DbType.Decimal);
            paymentParams.Add("Payer", payment.Payer);
            paymentParams.Add("Discriminator", payment.GetType().Name);
            paymentParams.Add("TransactionCode", payment.TransactionCode);

            var queryClient = @"INSERT INTO Payment (Id, SubscriptionId, PaidDate, ExpireDate, Total, TotalPaid, Payer, Discriminator, TransactionCode) 
                                VALUES (@Id, @SubscriptionId, @PaidDate, @ExpireDate, @Total, @TotalPaid, @Payer, @Discriminator, @TransactionCode)";

            await dbConnection.ExecuteAsync(queryClient, paymentParams);
        }

        public async Task<Payment> GetPaymentAsync(Guid clientId, Guid subscriptionId, Guid paymentId)
        {
            var paymentParams = new DynamicParameters();
            paymentParams.Add("Id", paymentId);
            paymentParams.Add("ClientId", clientId);
            paymentParams.Add("SubscriptionId", subscriptionId);

            var query = @"select p.Id, p.SubscriptionId, p.PaidDate, p.ExpireDate, p.Total, p.TotalPaid, p.Payer, p.CardHolderName, p.CardNumber, 
                        p.LastTransactionNumber, p.TransactionCode, p.Discriminator from Payment p 
                        inner join Subscription s on p.SubscriptionId = s.Id 
                        inner join Client c on s.ClientId = c.Id 
                        Where c.Id = @ClientId and s.Id = @SubscriptionId and p.Id = @Id";

            var reader = await dbConnection.ExecuteReaderAsync(query, paymentParams);

            if(reader != null)
            {
                while (reader.Read())
                {
                    if (reader.GetValue(11).ToString().Equals("PayPalPayment"))
                    {
                        var paypalPayment = GetPayPalPayment(reader);

                        paypalPayment.ClientId = clientId;

                        return paypalPayment;
                    }
                    else if (reader.GetValue(11).ToString().Equals("CreditCardPayment"))
                    {
                        var creditCardPayment = GetCreditCardPayment(reader);

                        creditCardPayment.ClientId = clientId;

                        return creditCardPayment;
                    }
                }
            }

            return null;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsFromSubscriptionAsync(Guid clientId, Guid subscriptionId)
        {
            ICollection<Payment> payments;

            var paymentParams = new DynamicParameters();
            paymentParams.Add("Id", subscriptionId);
            paymentParams.Add("ClientId", clientId);

            var query = @"select p.Id, p.SubscriptionId, p.PaidDate, p.ExpireDate, p.Total, p.TotalPaid, p.Payer, p.CardHolderName, p.CardNumber, p.LastTransactionNumber, p.TransactionCode, p.Discriminator 
                        from Payment p inner join Subscription s on p.SubscriptionId = s.Id 
                        Where s.ClientId = @ClientId and s.Id = @Id";

            var reader = await dbConnection.ExecuteReaderAsync(query, paymentParams);

            if (reader != null)
            {
                payments = new List<Payment>();

                while (reader.Read())
                {
                    if (reader.GetValue(11).ToString().Equals("PayPalPayment"))
                    {
                        var paypalPayment = GetPayPalPayment(reader);
                        paypalPayment.ClientId = clientId;

                        payments.Add(paypalPayment);
                    }
                    else if (reader.GetValue(11).ToString().Equals("CreditCardPayment"))
                    {
                        var creditCardPayment = GetCreditCardPayment(reader);
                        creditCardPayment.ClientId = clientId;

                        payments.Add(creditCardPayment);
                    }
                }

                return payments;
            }

            return null;
        }

        #region private
        PayPalPayment GetPayPalPayment(IDataReader reader)
        {
            var paypalPayment = new PayPalPayment();

            paypalPayment.Id = reader.GetGuid(0);
            paypalPayment.SubscriptionId = reader.GetGuid(1);
            paypalPayment.PaidDate = DateTimeOffset.Parse(reader.GetValue(2).ToString());
            paypalPayment.ExpireDate = DateTimeOffset.Parse(reader.GetValue(3).ToString());
            paypalPayment.Total = reader.GetDecimal(4);
            paypalPayment.TotalPaid = reader.GetDecimal(5);
            paypalPayment.Payer = reader.GetString(6);
            paypalPayment.TransactionCode = reader.GetString(10);

            return paypalPayment;
        }

        CreditCardPayment GetCreditCardPayment(IDataReader reader)
        {
            var creditCardPayment = new CreditCardPayment();

            creditCardPayment.Id = reader.GetGuid(0);
            creditCardPayment.SubscriptionId = reader.GetGuid(1);
            creditCardPayment.PaidDate = DateTimeOffset.Parse(reader.GetValue(2).ToString());
            creditCardPayment.ExpireDate = DateTimeOffset.Parse(reader.GetValue(3).ToString());
            creditCardPayment.Total = reader.GetDecimal(4);
            creditCardPayment.TotalPaid = reader.GetDecimal(5);
            creditCardPayment.Payer = reader.GetString(6);
            creditCardPayment.CardHolderName = reader.GetString(7);
            creditCardPayment.CardNumber = reader.GetString(8);
            creditCardPayment.LastTransactionNumber = reader.GetString(9);

            return creditCardPayment;
        }
        #endregion
    }
}
