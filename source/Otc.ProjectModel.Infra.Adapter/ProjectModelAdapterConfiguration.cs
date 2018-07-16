using Otc.ComponentModel.DataAnnotations;

namespace Otc.ProjectModel.Infra.Adapter
{
    public class ProjectModelAdapterConfiguration
    {
        [Required]
        public string mailUrl { get; set; }
    }
}
