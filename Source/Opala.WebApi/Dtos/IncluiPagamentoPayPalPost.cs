using System;

namespace Opala.WebApi.Dtos
{
    public class IncluiPagamentoPayPalPost
    {
        public Guid ClienteId { get; set; }
        public Guid AssinaturaId { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataExpiracao { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPago { get; set; }
        public string Pagador { get; set; }
        public string CodigoTransacao { get; set; }
    }
}
