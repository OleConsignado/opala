namespace Otc.ProjectModel.WebApi.Dtos
{
    public class UpdateClientPut
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public AddressPut Address { get; set; }

        /// <summary>
        /// Classe aninhada tratada somente como objeto de valor
        /// </summary>
        public class AddressPut
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