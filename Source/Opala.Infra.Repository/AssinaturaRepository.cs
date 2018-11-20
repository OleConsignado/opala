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
    public class AssinaturaRepository : IAssinaturaReadOnlyRepository, IAssinaturaWriteOnlyRepository
    {
        private readonly IDbConnection dbConnection;

        public AssinaturaRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task IncluiAssinaturaAsync(Assinatura assinatura)
        {
            if (assinatura == null)
                throw new ArgumentNullException(nameof(assinatura));

            var assinaturaParams = new DynamicParameters();
            assinaturaParams.Add("ClienteId", assinatura.ClienteId, DbType.Guid);
            assinaturaParams.Add("Id", assinatura.Id, DbType.Guid);
            assinaturaParams.Add("Nome", assinatura.Nome, DbType.AnsiString);
            assinaturaParams.Add("DataCriacao", assinatura.DataCriacao, DbType.DateTime);
            assinaturaParams.Add("DataUltimaAtualizacao", assinatura.DataUltimaAtualizacao, DbType.DateTime);
            assinaturaParams.Add("DataExpiracao", assinatura.DataExpiracao, DbType.DateTime);
            assinaturaParams.Add("Ativa", assinatura.Ativa, DbType.Byte);

            var queryAssinatura = @"INSERT INTO Assinatura (Id, ClienteId, Nome, DataCriacao, DataUltimaAtualizacao, DataExpiracao, Ativa) VALUES (@Id, @ClienteId, @Nome, @DataCriacao, @DataUltimaAtualizacao, @DataExpiracao, @Ativa)";

            await dbConnection.ExecuteAsync(queryAssinatura, assinaturaParams);
        }

        public async Task<Assinatura> RetornaAssinaturaAsync(Guid id)
        {
            var assinaturaParams = new DynamicParameters();
            assinaturaParams.Add("Id", id, DbType.Guid);

            var query = @"Select Id, ClienteId, Nome, DataCriacao, DataUltimaAtualizacao, DataExpiracao, Ativa From Assinatura with (nolock) Where Id = @Id";

            var assinatura = await dbConnection.QueryAsync<Assinatura>(query, assinaturaParams);

            return assinatura.SingleOrDefault();
        }

        public async Task<IEnumerable<Assinatura>> RetornaAssinaturasClienteAsync(Guid clienteId, int pagina, int totalRegistros)
        {
            var assinaturaParams = new DynamicParameters();
            assinaturaParams.Add("ClienteId", clienteId, DbType.Guid);
            assinaturaParams.Add("PageNumber", pagina);
            assinaturaParams.Add("RowsPerPage", totalRegistros);

            var query = @"Select Id, ClienteId, Nome, DataCriacao, DataUltimaAtualizacao, DataExpiracao, Ativa From 
                            (Select ROW_NUMBER() OVER(ORDER BY DataCriacao) AS RowNumber, Id, ClienteId, Nome, DataCriacao, DataUltimaAtualizacao, DataExpiracao, Ativa From Assinatura with (nolock) Where ClienteId = @ClienteId) As S
                          Where RowNumber BETWEEN ((@PageNumber - 1) * @RowsPerPage + 1) AND (@PageNumber * @RowsPerPage)";

            var assinaturas = await dbConnection.QueryAsync<Assinatura>(query, assinaturaParams);

            return assinaturas.ToList();
        }
    }
}