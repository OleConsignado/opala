using Otc.ComponentModel.DataAnnotations;
using System;

namespace Opala.Core.Domain.Models
{
    public abstract class Pagamento
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorKey = "400.001")]
        public Guid ClienteId { get; set; }

        [Required(ErrorKey = "400.002")]
        public Guid AssinaturaId { get; set; }

        public DateTimeOffset DataPagamento { get; set; }

        public DateTimeOffset DataExpiracao { get; set; }

        [Required(ErrorKey = "400.003")]
        public decimal Total { get; set; }

        [Required(ErrorKey = "400.004")]
        public decimal TotalPago { get; set; }

        [Required(ErrorKey = "400.005")]
        public string Pagador { get; set; }
    }
}
