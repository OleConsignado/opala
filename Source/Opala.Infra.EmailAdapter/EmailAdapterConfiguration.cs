using Otc.ComponentModel.DataAnnotations;

namespace Opala.Infra.EmailAdapter
{
    public class EmailAdapterConfiguration
    {
        [Required]
        public string Smtp { get; set; }

        [Required]
        public int Port { get; set; }

        public bool EnableSsl { get; set; }

        /// <summary>
        /// Put password in Base64 encode
        /// </summary>
        public string Password { get; set; }
    }
}
