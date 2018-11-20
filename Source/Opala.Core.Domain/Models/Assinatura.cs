using Otc.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Opala.Core.Domain.Models
{
    public class Assinatura
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorKey = "400.001")]
        public Guid ClienteId { get; set; }

        [Required(ErrorKey = "400.002")]
        public string Nome { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime? DataUltimaAtualizacao { get; set; }

        public DateTime? DataExpiracao { get; set; }

        public bool Ativa { get; set; }

        public ICollection<Pagamento> Pagamentos { get; set; }
    }
}
