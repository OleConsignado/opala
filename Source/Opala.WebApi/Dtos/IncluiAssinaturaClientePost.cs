using System;

namespace Opala.WebApi.Dtos
{
    public class IncluiAssinaturaClientePost
    {
        public Guid ClienteId { get; set; }

        public string Nome { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime? DataUltimaAtualizacao { get; set; }

        public DateTime? DataExpiracao { get; set; }

        public bool Ativa { get; set; }
    }
}
