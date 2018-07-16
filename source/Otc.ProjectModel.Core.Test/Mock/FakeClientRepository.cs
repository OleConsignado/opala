using Otc.ProjectModel.Core.Domain.Exceptions;
using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Repositories;
using Otc.ProjectModel.Core.Domain.ValueObjects;
using System;

namespace Otc.ProjectModel.Core.Test.Mock
{
    public class FakeClientRepository : IClientRepository
    {
        public void AddClient(Client client)
        {
            if (client == null)
                throw new NullReferenceException();
        }

        public void AddClientSubscription(Guid clientId, Subscription subscription)
        {
            var client = GetClient(Guid.Parse("5D502C13-8184-499E-8A02-A6C6A1C21188"));

            client.AddSubscription(subscription);
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
    }
}
