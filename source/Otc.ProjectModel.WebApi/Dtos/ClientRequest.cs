using Otc.ComponentModel.DataAnnotations;
using System;

namespace Otc.ProjectModel.WebApi.Dtos
{
    public class ClientRequest
    {
        public Guid Id { get; set; }

        [Required(ErrorKey = "400.001")]
        public string Name { get; set; }

        [Required(ErrorKey = "400.002")]
        [EmailAddress(ErrorKey = "400.003")]
        public string Email { get; set; }

        public Address Address { get; set; }
    }
}
