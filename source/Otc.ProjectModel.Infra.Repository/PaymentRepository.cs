using Dapper;
using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Repositories;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Infra.Repository
{
    public class PaymentRepository : IPaymentWriteOnlyRepository
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
    }
}
