using Otc.ComponentModel.DataAnnotations;

namespace Otc.ProjectModel.WebApi.Dtos
{
    public class PageOptions
    {
        [Range(1, int.MaxValue, ErrorKey = "400.001")]
        public int? Page { get; set; } = 1;

        [Range(1, 50, ErrorKey = "400.002")]
        public int? Count { get; set; } = 25;
    }
}
