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

            await new Task<Client>(() => new Client
            {
                Id = Guid.Parse(client.Id.ToString()),
                Name = client.Name,
                Email = client.Email,
                Address = client.Address
            });
        }

        public async Task<Client> GetClientAsync(Guid clientId)
        {
            if (clientId == Guid.Parse("5D502C13-8184-499E-8A02-A6C6A1C21188"))
            {
                return await new Task<Client>(() => new Client
                {
                    Id = Guid.Parse("5D502C13-8184-499E-8A02-A6C6A1C21188"),
                    Name = "Luciano",
                    Email = "teste@teste.com",
                    Address = new Address
                    {
                        Street = "Rua teste",
                        Number = "100",
                        Neighborhood = "Centro",
                        City = "Belo Horizonte",
                        State = "MG",
                        Country = "Brasil",
                        ZipCode = "123456"
                    }
                });
            }
            else
            {
                return null;
            }
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
