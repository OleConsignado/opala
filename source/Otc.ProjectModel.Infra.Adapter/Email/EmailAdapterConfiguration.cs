using Otc.ComponentModel.DataAnnotations;

namespace Otc.ProjectModel.Infra.Adapter.Email
{
    public class EmailAdapterConfiguration
    {
        [Required]
        public string Smtp { get; set; }

        [Required]
        public int Port { get; set; }
    }
}
