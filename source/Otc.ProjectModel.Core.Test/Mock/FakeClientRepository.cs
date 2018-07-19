using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace Otc.ProjectModel.Core.Test.Mock
{
    public class FakeClientRepository : IClientReadOnlyRepository, IClientWriteOnlyRepository
    {
        public async Task AddClientAsync(Client client)
        {
            if (client == null)
                throw new NullReferenceException();
        }

        public Client GetClient(Guid clientId)
        {
            if(clientId == Guid.Parse("5D502C13-8184-499E-8A02-A6C6A1C21188"))
            {
                return new Client
                {
                    Id = Guid.Parse("5D502C13-8184-499E-8A02-A6C6A1C21188"),
                    Name = "Luciano",
                    Email = "teste@teste.com",
                    Address = new Address
                    {
                        Street = "Rua dos Testes",
                        Number = "42",
                        Neighborhood = "Vizinhança",
                        City = "Megalópolis",
                        Country = "Brézil",
                        State = "LH",
                        ZipCode = "123456"
                    }
                };
            }

            return null;
        }

        public Task<Client> GetClientAsync(Guid clientId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClientAsync(Guid clientId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateClientAsync(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
