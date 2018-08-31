using Otc.ComponentModel.DataAnnotations;

namespace Otc.ProjectModel.Infra.Repository
{
    public class RepositoryConfiguration
    {
        [Required]
        public string SqlConnectionString { get; set; }
    }
}
