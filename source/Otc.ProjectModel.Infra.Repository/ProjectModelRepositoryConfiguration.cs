using Otc.ComponentModel.DataAnnotations;

namespace Otc.ProjectModel.Infra.Repository
{
    public class ProjectModelRepositoryConfiguration
    {
        [Required]
        public string SqlConnectionString { get; set; }
    }
}
