using Dapper;
using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Infra.Repository
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

            var query = @"Select Id, ClientId, Name, CreatedDate, LastUpdatedDate, ExpireDate, Active From Subscription Where Id = @Id";

            var subscription = await dbConnection.QueryAsync<Subscription>(query, subscriptionParams);

            return subscription.SingleOrDefault();
        }

        public async Task<IEnumerable<Subscription>> GetClientSubscriptionsAsync(Guid clientId)
        {
            // Atentar para a quantidade de registros retornadas
            // O ideal é paginar sua requisição ou filtrar o máximo possivel
            var subscriptionParams = new DynamicParameters();
            subscriptionParams.Add("ClientId", clientId, DbType.Guid);

            var query = @"Select top 100 Id, ClientId, Name, CreatedDate, LastUpdatedDate, ExpireDate, Active From Subscription Where ClientId = @ClientId";

            var subscriptions = await dbConnection.QueryAsync<Subscription>(query, subscriptionParams);

            return subscriptions.ToList();
        }
    }
}