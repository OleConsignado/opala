using System;

namespace Otc.ProjectModel.WebApi.Dtos
{
    public class ClientResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

       public AddressDto Address { get; set; }

    }
}
