using System;

namespace Opala.WebApi.Dtos
{
    public class IncluiPagamentoCartaoCreditoPost
    {
        public Guid ClienteId { get; set; }
        public Guid AssinaturaId { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataExpiracao { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPago { get; set; }
        public string Pagador { get; set; }
        public string NomeCartao { get; set; }
        public string Numero { get; set; }
        public string NumeroUltimaTransacao { get; set; }
    }
}
