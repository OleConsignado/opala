using Otc.ComponentModel.DataAnnotations;

namespace Otc.ProjectModel.Infra.EmailAdapter
{
    public class EmailAdapterConfiguration
    {
        [Required]
        public string Smtp { get; set; }

        [Required]
        [MinLength(1, ErrorKey = "400.001")]
        public int Port { get; set; }
    }
}
