using Otc.ComponentModel.DataAnnotations;

namespace Opala.Core.Domain.Models
{
    public class PayPalPayment : Payment
    {
        [Required(ErrorKey = "400.001")]
        public string TransactionCode { get; set; }
    }
}
