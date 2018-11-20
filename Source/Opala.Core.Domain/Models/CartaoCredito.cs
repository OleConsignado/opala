namespace Opala.Core.Domain.Models
{
    public class CartaoCredito : Pagamento
    {
        public string NomeCartao { get; set; }
        public string Numero { get; set; }
        public string NumeroUltimaTransacao { get; set; }
    }
}
