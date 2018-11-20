using Otc.ComponentModel.DataAnnotations;

namespace Opala.Core.Domain.Models
{
    public class PayPal : Pagamento
    {
        [Required(ErrorKey = "400.001")]
        public string CodigoTransacao { get; set; }
    }
}
