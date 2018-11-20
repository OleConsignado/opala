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
    public class ClienteRepository : IClienteReadOnlyRepository, IClienteWriteOnlyRepository
    {
        private readonly IDbConnection dbConnection;

        static ClienteRepository() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public ClienteRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public async Task IncluiClienteAsync(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente));

            var clienteParams = new DynamicParameters();
            clienteParams.Add("Id", cliente.Id);
            clienteParams.Add("Nome", cliente.Nome);
            clienteParams.Add("Email", cliente.Email);
            clienteParams.Add("Telefone", cliente.Telefone);
            clienteParams.Add("Ativo", cliente.Ativo, DbType.Boolean);
            clienteParams.Add("Excluido", false, DbType.Boolean);
            clienteParams.Add("Rua", cliente.Endereco.Rua);
            clienteParams.Add("Numero", cliente.Endereco.Numero);
            clienteParams.Add("Bairro", cliente.Endereco.Bairro);
            clienteParams.Add("Cidade", cliente.Endereco.Cidade);
            clienteParams.Add("Estado", cliente.Endereco.Estado);
            clienteParams.Add("Pais", cliente.Endereco.Pais);
            clienteParams.Add("Cep", cliente.Endereco.Cep);

            var queryCliente = @"INSERT INTO Cliente (Id, Nome, Email, Telefone, Excluido, Rua, Numero, Bairro, Cidade, Estado, Pais, Cep, Ativo) 
                                VALUES (@Id, @Nome, @Email, @Telefone, @Excluido, @Rua, @Numero, @Bairro, @Cidade, @Estado, @Pais, @Cep, @Ativo)";

            await dbConnection.ExecuteAsync(queryCliente, clienteParams);
        }

        public async Task<Cliente> RetornaClienteAsync(Guid clienteId)
        {
            var clienteParams = new DynamicParameters();
            clienteParams.Add("Id", clienteId);

            var query = @"select Id, Nome, Email, Telefone, Ativo, Rua, Numero, Bairro, Cidade, Estado, Pais, Cep from Cliente with (nolock) Where Id = @Id and Ativo = 1 and Excluido = 0";

            var cliente = await dbConnection.QueryAsync<Cliente, Endereco, Cliente>(query, (cli, add) => {
                cli.Endereco = add;
                return cli;
            }, clienteParams, splitOn: "Id,Rua");

            return cliente.SingleOrDefault();
        }

        public async Task<Cliente> RetornaClienteSemAssinaturaAsync(Guid clienteId)
        {
            var clientParams = new DynamicParameters();
            clientParams.Add("Id", clienteId);

            var query = @"select c.Id, c.Nome, c.Email, c.Telefone, c.Ativo, c.Rua, c.Numero, c.Bairro, c.Cidade, c.Estado, c.Pais, c.Cep,
                                s.ClienteId, s.Nome, s.DataCriacao, s.DataUltimaAtualizacao, s.DataExpiracao, s.Ativa 
                                from Cliente c with (nolock) left join Assinatura s with (nolock) on c.Id = s.ClienteId Where c.Id = @Id and c.Ativo = 1 and c.Excluido = 0";

            var assinaturas = new Dictionary<Guid, Cliente>();

            var cliente = await dbConnection.QueryAsync<Cliente, Endereco, Assinatura, Cliente>(query, (cli, add, subs) =>
            {
                cli.Endereco = add;

                if (!assinaturas.TryGetValue(cli.Id, out Cliente clienteEntry))
                {
                    clienteEntry = cli;
                    clienteEntry.Assinaturas = new List<Assinatura>();
                    assinaturas.Add(clienteEntry.Id, clienteEntry);
                }

                if (subs != null)
                    clienteEntry.Assinaturas.Add(subs);

                return clienteEntry;
            }, clientParams, splitOn: "Id,Rua,ClienteId");

            return cliente.FirstOrDefault();
        }

        public async Task AtivaDesativaClienteAsync(Guid clienteId, bool ativa)
        {
            var clienteParams = new DynamicParameters();
            clienteParams.Add("Id", clienteId);
            clienteParams.Add("Ativo", ativa, DbType.Boolean);

            var deleteClient = @"Update Cliente Set Ativo=@Ativo WHERE Id = @Id";

            await dbConnection.ExecuteAsync(deleteClient, clienteParams);
        }

        public async Task AtualizaClienteAsync(Cliente cliente)
        {
            var clienteParams = new DynamicParameters();
            clienteParams.Add("Id", cliente.Id);
            clienteParams.Add("Nome", cliente.Nome);
            clienteParams.Add("Email", cliente.Email);
            clienteParams.Add("Telefone", cliente.Telefone);
            clienteParams.Add("Rua", cliente.Endereco.Rua);
            clienteParams.Add("Numero", cliente.Endereco.Numero);
            clienteParams.Add("Bairro", cliente.Endereco.Bairro);
            clienteParams.Add("Cidade", cliente.Endereco.Cidade);
            clienteParams.Add("Estado", cliente.Endereco.Estado);
            clienteParams.Add("Pais", cliente.Endereco.Pais);
            clienteParams.Add("Cep", cliente.Endereco.Cep);


            var queryCliente = @"UPDATE Cliente SET Nome = @Nome, Email = @Email, Telefone = @Telefone, Rua = @Rua, Numero = @Numero, Bairro = @Bairro, Cidade = @Cidade, Estado = @Estado, Pais = @Pais, Cep = @Cep WHERE Id = @Id and Ativo = 1 and Excluido = 0";

            await dbConnection.ExecuteAsync(queryCliente, clienteParams);
        }

        public async Task<bool> ClienteExisteAsync(Guid clienteId)
        {
            var clienteParams = new DynamicParameters();
            clienteParams.Add("Id", clienteId);

            var query = @"select count(Id) from Cliente with (nolock) Where Id = @Id and Excluido = 0";

            var result = await dbConnection.ExecuteScalarAsync(query, clienteParams);

            return Convert.ToUInt16(result) > 0;
        }

        public async Task ExcluiClienteAsync(Guid clienteId)
        {
            var clienteParams = new DynamicParameters();
            clienteParams.Add("Id", clienteId);
            clienteParams.Add("Date", DateTimeOffset.Now, DbType.DateTimeOffset);


            var query = @"update Cliente set Ativo = 0, Excluido = 1, DataExclusao = @Date Where Id = @Id";

            await dbConnection.ExecuteScalarAsync<int>(query, clienteParams);
        }

        public async Task<IEnumerable<Cliente>> ListaClientesAsync()
        {
            var query = @"select Id, Nome, Email, Telefone, Ativo, Rua, Numero, Bairro, Cidade, Estado, Pais, Cep from Cliente with (nolock) Where Ativo = 1 and Excluido = 0";

            var clientes = await dbConnection.QueryAsync<Cliente, Endereco, Cliente>(query, (cli, add) => {
                cli.Endereco = add;
                return cli;
            }, null, splitOn: "Id,Rua");

            return clientes;
        }
    }
}