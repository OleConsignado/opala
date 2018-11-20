using Otc.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Opala.Core.Domain.Models
{
    public class Cliente
    {
        public ICollection<Assinatura> Assinaturas { get; set; }

        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorKey = "400.001")]
        [MinLength(3, ErrorKey = "400.002")]
        public string Nome { get; set; }

        [EmailAddress(ErrorKey = "400.003")]
        public string Email { get; set; }

        public bool Ativo { get; set; }

        public Endereco Endereco { get; set; }

        [Required(ErrorKey = "400.004")]
        public string Telefone { get; set; }
    }
}
