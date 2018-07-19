using Otc.ComponentModel.DataAnnotations;

namespace Otc.ProjectModel.Core.Application
{
    public class ApplicationConfiguration
    {
        [Required]
        public string EmailFrom { get; set; }
    }
}
