using Dapper;
using Opala.Core.Domain.Models;
using Opala.Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Opala.Infra.Repository
{
    public class SubscriptionRepository : ISubscriptionReadOnlyRepository, ISubscriptionWriteOnlyRepository
    {
        private readonly IDbConnection dbConnection;

        public SubscriptionRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task AddSubscriptionAsync(Subscription subscription)
        {
            if (subscription == null)
                throw new ArgumentNullException(nameof(subscription));

            var subscriptionParams = new DynamicParameters();
            subscriptionParams.Add("ClientId", subscription.ClientId, DbType.Guid);
            subscriptionParams.Add("Id", subscription.Id, DbType.Guid);
            subscriptionParams.Add("Name", subscription.Name, DbType.AnsiString);
            subscriptionParams.Add("CreatedDate", subscription.CreatedDate, DbType.DateTime);
            subscriptionParams.Add("LastUpdatedDate", subscription.LastUpdatedDate, DbType.DateTime);
            subscriptionParams.Add("ExpireDate", subscription.ExpireDate, DbType.DateTime);
            subscriptionParams.Add("Active", subscription.Active, DbType.Byte);

            var querySubscription = @"INSERT INTO Subscription (Id, ClientId, Name, CreatedDate, LastUpdatedDate, ExpireDate, Active) VALUES (@Id, @ClientId, @Name, @CreatedDate, @LastUpdatedDate, @ExpireDate, @Active)";

            await dbConnection.ExecuteAsync(querySubscription, subscriptionParams);
        }

        public async Task<Subscription> GetSubscriptionAsync(Guid id)
        {
            var subscriptionParams = new DynamicParameters();
            subscriptionParams.Add("Id", id, DbType.Guid);

            var query = @"Select Id, ClientId, Name, CreatedDate, LastUpdatedDate, ExpireDate, Active From Subscription with (nolock) Where Id = @Id";

            var subscription = await dbConnection.QueryAsync<Subscription>(query, subscriptionParams);

            return subscription.SingleOrDefault();
        }

        public async Task<IEnumerable<Subscription>> GetClientSubscriptionsAsync(Guid clientId, int page, int count)
        {
            var subscriptionParams = new DynamicParameters();
            subscriptionParams.Add("ClientId", clientId, DbType.Guid);
            subscriptionParams.Add("PageNumber", page);
            subscriptionParams.Add("RowsPerPage", count);

            var query = @"Select Id, ClientId, Name, CreatedDate, LastUpdatedDate, ExpireDate, Active From 
                            (Select ROW_NUMBER() OVER(ORDER BY CreatedDate) AS RowNumber, Id, ClientId, Name, CreatedDate, LastUpdatedDate, ExpireDate, Active From Subscription with (nolock) Where ClientId = @ClientId) As S
                          Where RowNumber BETWEEN ((@PageNumber - 1) * @RowsPerPage + 1) AND (@PageNumber * @RowsPerPage)";

            var subscriptions = await dbConnection.QueryAsync<Subscription>(query, subscriptionParams);

            return subscriptions.ToList();
        }
    }
}