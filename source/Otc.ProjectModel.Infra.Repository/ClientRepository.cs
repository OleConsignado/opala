using Dapper;
using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Repositories;
using System;
using System.Data;
using System.Linq;
using System.Transactions;

namespace Otc.ProjectModel.Infra.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly IDbConnection _dbConnection;

        public ClientRepository(IDbConnection dbConnection)
        {
            this._dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public void AddClient(Client client)
        {
            var clientParams = new DynamicParameters();
            clientParams.Add("Id", client.Id, DbType.Guid);
            clientParams.Add("Nome", client.Name, DbType.AnsiString);
            clientParams.Add("Email", client.Email, DbType.AnsiString);

            var addressParams = new DynamicParameters();
            addressParams.Add("Address.ClientId", client.Id, DbType.Guid);
            addressParams.Add("Address.Street", client.Address.Street, DbType.AnsiString);
            addressParams.Add("Address.Number", client.Address.Number, DbType.AnsiString);
            addressParams.Add("Address.Neighborhood", client.Address.Neighborhood, DbType.AnsiString);
            addressParams.Add("Address.City", client.Address.City, DbType.AnsiString);
            addressParams.Add("Address.State", client.Address.State, DbType.AnsiString);
            addressParams.Add("Address.Country", client.Address.Country, DbType.AnsiString);
            addressParams.Add("Address.ZipCode", client.Address.ZipCode, DbType.AnsiString);

            var queryClient = @"INSERT INTO Client (Id, Name, Email) VALUES (@Id, @Name, @Email)";
            var queryClientAddress = @"INSERT INTO Address (ClientId, Street, Number, Neighborhood, City, State, Country, ZipCode) VALUES (@ClientId, @Street, @Number, @Neighborhood, @City, @State, @Country, @ZipCode)";

            using (var trans = new TransactionScope())
            {
                if (_dbConnection.Execute(queryClient, clientParams) > 0)
                    if (_dbConnection.Execute(queryClientAddress, addressParams) > 0)
                        trans.Complete();
            }
        }

        public Client GetClient(Guid clientId)
        {
            var clientParams = new DynamicParameters();
            clientParams.Add("Id", clientId, DbType.Guid);

            var query = @"SELECT ClientId, Street, Number, Neighborhood, City, State, Country, ZipCode FROM Clients Where ClientId = @ClientId";

            var client = _dbConnection.Query<Client>(query, clientParams).SingleOrDefault();

            return client;
        }

        public void RemoveClient(Guid clientId)
        {
            var clientParams = new DynamicParameters();
            clientParams.Add("Id", clientId, DbType.Guid);

            var deleteAddress = @"DELETE FROM Address WHERE ClientId = @ClientId";
            var deletePayments = @"DELETE FROM Payments WHERE SubscriptionId = @SubscriptionId";
            var deleteSubscriptions = @"DELETE FROM Subscriptions WHERE ClientId = @ClientId";
            var deleteClient = @"DELETE FROM Clients WHERE ClientId = @ClientId";

            var selectSubscritionsId = @"SELECT SubscritionId WHERE ClientId = @ClientID";

            using (var trans = _dbConnection.BeginTransaction())
            {

                var subscritionsId = _dbConnection.Query<Guid>(selectSubscritionsId).ToList();

                foreach (var subId in subscritionsId)
                    _dbConnection.Execute(deletePayments, new { Number = subId });

                _dbConnection.Execute(deleteSubscriptions, clientParams);
                _dbConnection.Execute(deleteAddress, clientParams);
                _dbConnection.Execute(deleteClient, clientParams);

                trans.Commit();
            }
        }

        public void UpdateClient(Client client)
        {
            var clientParams = new DynamicParameters();
            clientParams.Add("Id", client.Id, DbType.Guid);
            clientParams.Add("Nome", client.Name, DbType.AnsiString);
            clientParams.Add("Email", client.Email, DbType.AnsiString);

            var addressParams = new DynamicParameters();
            addressParams.Add("Address.ClientId", client.Id, DbType.Guid);
            addressParams.Add("Address.Street", client.Address.Street, DbType.AnsiString);
            addressParams.Add("Address.Number", client.Address.Number, DbType.AnsiString);
            addressParams.Add("Address.Neighborhood", client.Address.Neighborhood, DbType.AnsiString);
            addressParams.Add("Address.City", client.Address.City, DbType.AnsiString);
            addressParams.Add("Address.State", client.Address.State, DbType.AnsiString);
            addressParams.Add("Address.Country", client.Address.Country, DbType.AnsiString);
            addressParams.Add("Address.ZipCode", client.Address.ZipCode, DbType.AnsiString);

            var queryClient = @"UPDATE Client SET Name = @Name, Email = @Email) VALUES (@Name, @Email) WHERE ClientId = @ClientId";
            var queryClientAddress = @"UPDATE Address SET Street = @Street, Number = @Number, Neighborhood = @Neighborhood, City = @City, State = @State, Country = @Country, ZipCode = @ZipCode WHERE ClientId = @ClientId";

            using (var trans = new TransactionScope())
            {
                if (_dbConnection.Execute(queryClient, clientParams) > 0)
                    if (_dbConnection.Execute(queryClientAddress, addressParams) > 0)
                        trans.Complete();
            }
        }
    }
}
