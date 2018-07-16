using Dapper;
using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Repositories;
using System;
using System.Data;
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

            var queryClient = @"INSERT INTO CLIENT (Id, Name, Email) VALUES (@Id, @Name, @Email)";
            var queryClientAddress = @"INSERT INTO ADDRESS (ClientId, Street, Number, Neighborhood, City, State, Country, ZipCode) VALUES (@ClientId, @Street, @Number, @Neighborhood, @City, @State, @Country, @ZipCode)";

            using (var trans = new TransactionScope())
            {
                if (_dbConnection.Execute(queryClient, clientParams) > 0)
                    if (_dbConnection.Execute(queryClientAddress, addressParams) > 0)
                        trans.Complete();
            }
        }

        public void AddClientSubscription(Client client)
        {
            throw new NotImplementedException();
        }

        public bool EmailExists(string email)
        {
            throw new NotImplementedException();
        }

        public Client GetClient(Guid clientId)
        {
            throw new NotImplementedException();
        }
    }
}
