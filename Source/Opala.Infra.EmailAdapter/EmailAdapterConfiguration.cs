using Otc.ComponentModel.DataAnnotations;

namespace Opala.Infra.EmailAdapter
{
    public class EmailAdapterConfiguration
    {
        [Required]
        public string Smtp { get; set; }

        [Required]
        public int Porta { get; set; }

        public bool HabilitaSsl { get; set; }

        /// <summary>
        /// Put password in Base64 encode
        /// </summary>
        public string Senha { get; set; }
    }
}
