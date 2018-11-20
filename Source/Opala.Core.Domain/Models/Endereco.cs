using Otc.ComponentModel.DataAnnotations;

namespace Opala.Core.Domain.Models
{
    public class Endereco
    {
        [Required(ErrorKey = "400.001")]
        public string Rua { get; set; }

        [Required(ErrorKey = "400.002")]
        public string Numero { get; set; }

        [Required(ErrorKey = "400.003")]
        public string Bairro { get; set; }

        [Required(ErrorKey = "400.004")]
        public string Cidade { get; set; }

        [Required(ErrorKey = "400.005")]
        public string Estado { get; set; }

        [Required(ErrorKey = "400.006")]
        public string Pais { get; set; }

        [Required(ErrorKey = "400.007")]
        public string Cep { get; set; }
    }
}
