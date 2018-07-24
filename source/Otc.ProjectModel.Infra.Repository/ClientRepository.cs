using Dapper;
using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Otc.ProjectModel.Infra.Repository
{
    public class ClientRepository : IClientReadOnlyRepository, IClientWriteOnlyRepository
    {
        private readonly IDbConnection dbConnection;

        public ClientRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);
        }

        public async Task AddClientAsync(Client client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            var clientParams = new DynamicParameters();
            clientParams.Add("Id", client.Id, DbType.Guid);
            clientParams.Add("Name", client.Name, DbType.AnsiString);
            clientParams.Add("Email", client.Email, DbType.AnsiString);

            var addressParams = new DynamicParameters();
            addressParams.Add("ClientId", client.Id, DbType.Guid);
            addressParams.Add("Street", client.Address.Street, DbType.AnsiString);
            addressParams.Add("Number", client.Address.Number, DbType.AnsiString);
            addressParams.Add("Neighborhood", client.Address.Neighborhood, DbType.AnsiString);
            addressParams.Add("City", client.Address.City, DbType.AnsiString);
            addressParams.Add("State", client.Address.State, DbType.AnsiString);
            addressParams.Add("Country", client.Address.Country, DbType.AnsiString);
            addressParams.Add("ZipCode", client.Address.ZipCode, DbType.AnsiString);

            var queryClient = @"INSERT INTO Client (Id, Name, Email) VALUES (@Id, @Name, @Email)";
            var queryClientAddress = @"INSERT INTO Address (ClientId, Street, Number, Neighborhood, City, State, Country, ZipCode) VALUES (@ClientId, @Street, @Number, @Neighborhood, @City, @State, @Country, @ZipCode)";

            using (var trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (await dbConnection.ExecuteAsync(queryClient, clientParams) > 0)
                    if (await dbConnection.ExecuteAsync(queryClientAddress, addressParams) > 0)
                        trans.Complete();
            }
        }

        public async Task<Client> GetClientAsync(Guid clientId)
        {
            var clientParams = new DynamicParameters();
            clientParams.Add("Id", clientId, DbType.Guid);

            var query = @"select Id, Name, Email, ClientId, Street, Number, Neighborhood, City, State, Country, 
                        ZipCode from Client c inner join Address a on c.Id = a.ClientId Where c.Id = @Id";

            var client = await dbConnection.QueryAsync<Client, Address, Client>(query, (cli, add) => {
                cli.Address = add;
                return cli;
            }, clientParams, splitOn: "Id,ClientId");

            return client.SingleOrDefault();
        }

        public async Task RemoveClientAsync(Guid clientId)
        {
            var clientParams = new DynamicParameters();
            clientParams.Add("Id", clientId, DbType.Guid);

            var deleteAddress = @"DELETE FROM Address WHERE ClientId = @ClientId";
            var deletePayments = @"DELETE FROM Payments WHERE SubscriptionId = @SubscriptionId";
            var deleteSubscriptions = @"DELETE FROM Subscriptions WHERE ClientId = @ClientId";
            var deleteClient = @"DELETE FROM Clients WHERE ClientId = @ClientId";

            var selectSubscritionsId = @"SELECT SubscritionId WHERE ClientId = @ClientID";

            using (var trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var subscritionsId = await dbConnection.QueryAsync<Guid>(selectSubscritionsId);

                foreach (var subId in subscritionsId.ToList())
                    await dbConnection.ExecuteAsync(deletePayments, new { Number = subId });

                await dbConnection.ExecuteAsync(deleteSubscriptions, clientParams);
                await dbConnection.ExecuteAsync(deleteAddress, clientParams);
                await dbConnection.ExecuteAsync(deleteClient, clientParams);

                trans.Complete();
            }
        }

        public async Task UpdateClientAsync(Client client)
        {
            var clientParams = new DynamicParameters();
            clientParams.Add("Id", client.Id, DbType.Guid);
            clientParams.Add("Name", client.Name, DbType.AnsiString);
            clientParams.Add("Email", client.Email, DbType.AnsiString);

            var addressParams = new DynamicParameters();
            addressParams.Add("ClientId", client.Id, DbType.Guid);
            addressParams.Add("Street", client.Address.Street, DbType.AnsiString);
            addressParams.Add("Number", client.Address.Number, DbType.AnsiString);
            addressParams.Add("Neighborhood", client.Address.Neighborhood, DbType.AnsiString);
            addressParams.Add("City", client.Address.City, DbType.AnsiString);
            addressParams.Add("State", client.Address.State, DbType.AnsiString);
            addressParams.Add("Country", client.Address.Country, DbType.AnsiString);
            addressParams.Add("ZipCode", client.Address.ZipCode, DbType.AnsiString);

            var queryClient = @"UPDATE Client SET Name = @Name, Email = @Email WHERE Id = @Id";
            var queryClientAddress = @"UPDATE Address SET Street = @Street, Number = @Number, Neighborhood = @Neighborhood, City = @City, State = @State, Country = @Country, ZipCode = @ZipCode WHERE ClientId = @ClientId";

            using (var trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (await dbConnection.ExecuteAsync(queryClient, clientParams) > 0)
                    if (await dbConnection.ExecuteAsync(queryClientAddress, addressParams) > 0)
                        trans.Complete();
            }
        }
    }
}
