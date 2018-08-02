using System;

namespace Otc.ProjectModel.WebApi.Dtos
{
    public class GetClientResult
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }

       public AddressGet Address { get; set; }

        /// <summary>
        /// Classe aninhada tratada somente como objeto de valor
        /// </summary>
        public class AddressGet
        {
            public string Street { get; set; }

            public string Number { get; set; }

            public string Neighborhood { get; set; }

            public string City { get; set; }

            public string State { get; set; }

            public string Country { get; set; }

            public string ZipCode { get; set; }
        }
    }
}