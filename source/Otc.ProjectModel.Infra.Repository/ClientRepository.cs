using Dapper;
using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Repositories;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Infra.Repository
{
    public class ClientRepository : IClientReadOnlyRepository, IClientWriteOnlyRepository
    {
        private readonly IDbConnection dbConnection;

        static ClientRepository() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public ClientRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public async Task AddClientAsync(Client client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            var clientParams = new DynamicParameters();
            clientParams.Add("Id", client.Id, DbType.Guid);
            clientParams.Add("Name", client.Name, DbType.AnsiString);
            clientParams.Add("Email", client.Email, DbType.AnsiString);
            clientParams.Add("IsActive", client.IsActive, DbType.Boolean);
            clientParams.Add("Street", client.Address.Street, DbType.AnsiString);
            clientParams.Add("Number", client.Address.Number, DbType.AnsiString);
            clientParams.Add("Neighborhood", client.Address.Neighborhood, DbType.AnsiString);
            clientParams.Add("City", client.Address.City, DbType.AnsiString);
            clientParams.Add("State", client.Address.State, DbType.AnsiString);
            clientParams.Add("Country", client.Address.Country, DbType.AnsiString);
            clientParams.Add("ZipCode", client.Address.ZipCode, DbType.AnsiString);

            var queryClient = @"INSERT INTO Client (Id, Name, Email, Street, Number, Neighborhood, City, State, Country, ZipCode, IsActive) 
                                VALUES (@Id, @Name, @Email, @Street, @Number, @Neighborhood, @City, @State, @Country, @ZipCode, @IsActive)";

            await dbConnection.ExecuteAsync(queryClient, clientParams);
        }

        public async Task<Client> GetClientAsync(Guid clientId)
        {
            var clientParams = new DynamicParameters();
            clientParams.Add("Id", clientId, DbType.Guid);

            var query = @"select Id, Name, Email, IsActive, Street, Number, Neighborhood, City, State, Country, ZipCode from Client Where Id = @Id";

            var client = await dbConnection.QueryAsync<Client, Address, Client>(query, (cli, add) => {
                cli.Address = add;
                return cli;
            }, clientParams, splitOn: "Id,Street");

            return client.SingleOrDefault();
        }

        public async Task EnableDisableClientAsync(Guid clientId, bool isActive)
        {
            var clientParams = new DynamicParameters();
            clientParams.Add("Id", clientId, DbType.Guid);
            clientParams.Add("IsActive", isActive, DbType.Boolean);

            var deleteClient = @"Update Client Set IsActive=@IsActive WHERE Id = @Id";

            await dbConnection.ExecuteAsync(deleteClient, clientParams);
        }

        public async Task UpdateClientAsync(Client client)
        {
            var clientParams = new DynamicParameters();
            clientParams.Add("Id", client.Id, DbType.Guid);
            clientParams.Add("Name", client.Name, DbType.AnsiString);
            clientParams.Add("Email", client.Email, DbType.AnsiString);
            clientParams.Add("Street", client.Address.Street, DbType.AnsiString);
            clientParams.Add("Number", client.Address.Number, DbType.AnsiString);
            clientParams.Add("Neighborhood", client.Address.Neighborhood, DbType.AnsiString);
            clientParams.Add("City", client.Address.City, DbType.AnsiString);
            clientParams.Add("State", client.Address.State, DbType.AnsiString);
            clientParams.Add("Country", client.Address.Country, DbType.AnsiString);
            clientParams.Add("ZipCode", client.Address.ZipCode, DbType.AnsiString);

            var queryClient = @"UPDATE Client SET Name = @Name, Email = @Email, Street = @Street, Number = @Number, Neighborhood = @Neighborhood, City = @City, State = @State, Country = @Country, ZipCode = @ZipCode WHERE Id = @Id";

            await dbConnection.ExecuteAsync(queryClient, clientParams);
        }
    }
}