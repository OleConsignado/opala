using Otc.ComponentModel.DataAnnotations;

namespace Otc.ProjectModel.WebApi.Dtos
{
    public class AddressDto
    {
        [Required(ErrorKey = "400.001")]
        public string Street { get; set; }

        [Required(ErrorKey = "400.002")]
        public string Number { get; set; }

        [Required(ErrorKey = "400.003")]
        public string Neighborhood { get; set; }

        [Required(ErrorKey = "400.004")]
        public string City { get; set; }

        [Required(ErrorKey = "400.005")]
        public string State { get; set; }

        [Required(ErrorKey = "400.006")]
        public string Country { get; set; }

        [Required(ErrorKey = "400.007")]
        public string ZipCode { get; set; }

    }
}
