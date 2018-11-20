using Dapper;
using Opala.Core.Domain.Models;
using Opala.Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Opala.Infra.Repository
{
    public class PagamentoRepository : IPagamentoWriteOnlyRepository, IPagamentoReadOnlyRepository
    {
        private readonly IDbConnection dbConnection;

        static PagamentoRepository() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public PagamentoRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public async Task IncluiPagamentoCartaoCreditoAsync(CartaoCredito pagamentoCartaoCredito)
        {
            if (pagamentoCartaoCredito == null)
                throw new ArgumentNullException(nameof(pagamentoCartaoCredito));

            var pagamentoParams = new DynamicParameters();
            pagamentoParams.Add("Id", pagamentoCartaoCredito.Id);
            pagamentoParams.Add("AssinaturaId", pagamentoCartaoCredito.AssinaturaId);
            pagamentoParams.Add("DataPagamento", pagamentoCartaoCredito.DataPagamento, DbType.DateTimeOffset);
            pagamentoParams.Add("DataExpiracao", pagamentoCartaoCredito.DataExpiracao, DbType.DateTimeOffset);
            pagamentoParams.Add("Total", pagamentoCartaoCredito.Total, DbType.Decimal);
            pagamentoParams.Add("TotalPago", pagamentoCartaoCredito.TotalPago, DbType.Decimal);
            pagamentoParams.Add("Pagador", pagamentoCartaoCredito.Pagador);
            pagamentoParams.Add("FormaPagamento", pagamentoCartaoCredito.GetType().Name);
            pagamentoParams.Add("NomeCartao", pagamentoCartaoCredito.NomeCartao);
            pagamentoParams.Add("Numero", pagamentoCartaoCredito.Numero);
            pagamentoParams.Add("NumeroUltimaTransacao", pagamentoCartaoCredito.NumeroUltimaTransacao);

            var queryPagamento = @"INSERT INTO Pagamento (Id, AssinaturaId, DataPagamento, DataExpiracao, Total, TotalPago, Pagador, FormaPagamento, NomeCartao, Numero, NumeroUltimaTransacao) 
                                VALUES (@Id, @AssinaturaId, @DataPagamento, @DataExpiracao, @Total, @TotalPago, @Pagador, @FormaPagamento, @NomeCartao, @Numero, @NumeroUltimaTransacao)";

            await dbConnection.ExecuteAsync(queryPagamento, pagamentoParams);
        }

        public async Task IncluiPagamentoPayPalAsync(PayPal pagamentoPayPal)
        {
            if (pagamentoPayPal == null)
                throw new ArgumentNullException(nameof(pagamentoPayPal));

            var pagamentoParams = new DynamicParameters();
            pagamentoParams.Add("Id", pagamentoPayPal.Id);
            pagamentoParams.Add("AssinaturaId", pagamentoPayPal.AssinaturaId);
            pagamentoParams.Add("DataPagamento", pagamentoPayPal.DataPagamento, DbType.DateTimeOffset);
            pagamentoParams.Add("DataExpiracao", pagamentoPayPal.DataExpiracao, DbType.DateTimeOffset);
            pagamentoParams.Add("Total", pagamentoPayPal.Total, DbType.Decimal);
            pagamentoParams.Add("TotalPago", pagamentoPayPal.TotalPago, DbType.Decimal);
            pagamentoParams.Add("Pagador", pagamentoPayPal.Pagador);
            pagamentoParams.Add("FormaPagamento", pagamentoPayPal.GetType().Name);
            pagamentoParams.Add("CodigoTransacao", pagamentoPayPal.CodigoTransacao);

            var queryPagamento = @"INSERT INTO Pagamento (Id, AssinaturaId, DataPagamento, DataExpiracao, Total, TotalPago, Pagador, FormaPagamento, CodigoTransacao) 
                                VALUES (@Id, @AssinaturaId, @DataPagamento, @DataExpiracao, @Total, @TotalPago, @Pagador, @FormaPagamento, @CodigoTransacao)";

            await dbConnection.ExecuteAsync(queryPagamento, pagamentoParams);
        }

        public async Task<Pagamento> RetornaPagamentoAsync(Guid clienteId, Guid assinaturaId, Guid pagamentoId)
        {
            var pagamentoParams = new DynamicParameters();
            pagamentoParams.Add("Id", pagamentoId);
            pagamentoParams.Add("ClienteId", clienteId);
            pagamentoParams.Add("AssinaturaId", assinaturaId);

            var query = @"select p.Id, p.AssinaturaId, p.DataPagamento, p.DataExpiracao, p.Total, p.TotalPago, p.Pagador, p.NomeCartao, p.Numero, p.NumeroUltimaTransacao, p.CodigoTransacao, p.FormaPagamento from Pagamento p with (nolock) 
                        inner join Assinatura s with (nolock) on p.AssinaturaId = s.Id 
                        inner join Cliente c with (nolock) on s.ClienteId = c.Id 
                        Where c.Id = @ClienteId and s.Id = @AssinaturaId and p.Id = @Id";

            var reader = await dbConnection.ExecuteReaderAsync(query, pagamentoParams);

            if(reader != null)
            {
                while (reader.Read())
                {
                    if (reader.GetValue(11).ToString().Equals("PayPal"))
                    {
                        var pagamentoPayPal = RetornaPagamentoPayPal(reader);

                        pagamentoPayPal.ClienteId = clienteId;

                        return pagamentoPayPal;
                    }
                    else if (reader.GetValue(11).ToString().Equals("CartaoCredito"))
                    {
                        var pagamentoCartaoCredito = RetornaPagamentoCartaoCredito(reader);

                        pagamentoCartaoCredito.ClienteId = clienteId;

                        return pagamentoCartaoCredito;
                    }
                }
            }

            return null;
        }

        public async Task<IEnumerable<Pagamento>> RetornaPagamentosAssinaturaAsync(Guid clienteId, Guid assinaturaId)
        {
            ICollection<Pagamento> pagamentos;

            var pagamentoParams = new DynamicParameters();
            pagamentoParams.Add("Id", assinaturaId);
            pagamentoParams.Add("ClienteId", clienteId);

            var query = @"select p.Id, p.AssinaturaId, p.DataPagamento, p.DataExpiracao, p.Total, p.TotalPago, p.Pagador, p.NomeCartao, p.Numero, p.NumeroUltimaTransacao, p.CodigoTransacao, p.FormaPagamento 
                        from Pagamento p with (nolock) inner join Assinatura s with (nolock) on p.AssinaturaId = s.Id 
                        Where s.ClienteId = @ClienteId and s.Id = @Id";

            var reader = await dbConnection.ExecuteReaderAsync(query, pagamentoParams);

            if (reader != null)
            {
                pagamentos = new List<Pagamento>();

                while (reader.Read())
                {
                    if (reader.GetValue(11).ToString().Equals("PayPal"))
                    {
                        var pagamentoPayPal = RetornaPagamentoPayPal(reader);
                        pagamentoPayPal.ClienteId = clienteId;

                        pagamentos.Add(pagamentoPayPal);
                    }
                    else if (reader.GetValue(11).ToString().Equals("CartaoCredito"))
                    {
                        var pagamentoCartaoCredito = RetornaPagamentoCartaoCredito(reader);
                        pagamentoCartaoCredito.ClienteId = clienteId;

                        pagamentos.Add(pagamentoCartaoCredito);
                    }
                }

                return pagamentos;
            }

            return null;
        }

        #region private
        PayPal RetornaPagamentoPayPal(IDataReader reader)
        {
            var payPal = new PayPal
            {
                Id = reader.GetGuid(0),
                AssinaturaId = reader.GetGuid(1),
                DataPagamento = DateTimeOffset.Parse(reader.GetValue(2).ToString()),
                DataExpiracao = DateTimeOffset.Parse(reader.GetValue(3).ToString()),
                Total = reader.GetDecimal(4),
                TotalPago = reader.GetDecimal(5),
                Pagador = reader.GetString(6),
                CodigoTransacao = reader.GetString(10)
            };

            return payPal;
        }

        CartaoCredito RetornaPagamentoCartaoCredito(IDataReader reader)
        {
            var cartaoCredito = new CartaoCredito
            {
                Id = reader.GetGuid(0),
                AssinaturaId = reader.GetGuid(1),
                DataPagamento = DateTimeOffset.Parse(reader.GetValue(2).ToString()),
                DataExpiracao = DateTimeOffset.Parse(reader.GetValue(3).ToString()),
                Total = reader.GetDecimal(4),
                TotalPago = reader.GetDecimal(5),
                Pagador = reader.GetString(6),
                NomeCartao = reader.GetString(7),
                Numero = reader.GetString(8),
                NumeroUltimaTransacao = reader.GetString(9)
            };

            return cartaoCredito;
        }
        #endregion
    }
}
