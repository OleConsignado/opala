using Otc.ComponentModel.DataAnnotations;

namespace Opala.Core.Application
{
    public class ApplicationConfiguration
    {
        [Required]
        public string EmailFrom { get; set; }
    }
}
