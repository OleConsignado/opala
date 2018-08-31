using Otc.ComponentModel.DataAnnotations;

namespace Opala.Infra.Repository
{
    public class RepositoryConfiguration
    {
        [Required]
        public string SqlConnectionString { get; set; }
    }
}
