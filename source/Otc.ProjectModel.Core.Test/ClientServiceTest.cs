using Microsoft.Extensions.DependencyInjection;
using Moq;
using Otc.DomainBase.Exceptions;
using Otc.ProjectModel.Core.Application;
using Otc.ProjectModel.Core.Domain.Adapters;
using Otc.ProjectModel.Core.Domain.Exceptions;
using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Repositories;
using Otc.ProjectModel.Core.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Otc.ProjectModel.Core.Test
{
    public class ClientServiceTest
    {
        private readonly ServiceProvider serviceProvider;
        private Guid clientId = Guid.Parse("4781C571-C246-470B-9CD5-13D3D169E799");

        public ClientServiceTest()
        {
            var clientReadOnlyRepoMock = new Mock<IClientReadOnlyRepository>();
            var clientWriteOnlyRepoMock = new Mock<IClientWriteOnlyRepository>();
            var subscriptionReadOnlyRepoMock = new Mock<ISubscriptionReadOnlyRepository>();
            var subscriptionWriteOnlyRepoMock = new Mock<ISubscriptionWriteOnlyRepository>();

            var emailAdapter = new Mock<IEmailAdapter>();
            var notificationAdapter = new Mock<INotificationAdapter>();

            ClientReadOnlyRepositoryMock(clientReadOnlyRepoMock);
            ClientReadWriteRepositoryMock(clientWriteOnlyRepoMock);

            SubscriptionWriteOnlyRepository(subscriptionWriteOnlyRepoMock);
            SubscriptionReadOnlyRepository(subscriptionReadOnlyRepoMock);

            IServiceCollection services = new ServiceCollection();

            services.AddLogging();
            services.AddTransient(t => clientReadOnlyRepoMock.Object);

            services.AddScoped(c => emailAdapter.Object);
            services.AddScoped(c => notificationAdapter.Object);

            services.AddTransient(t => clientWriteOnlyRepoMock.Object);
            services.AddTransient(t => clientWriteOnlyRepoMock.Object);
            services.AddTransient(t => subscriptionReadOnlyRepoMock.Object);
            services.AddTransient(t => subscriptionWriteOnlyRepoMock.Object);

            services.AddProjectModelCoreApplication(new ApplicationConfiguration { EmailFrom = "meu.teste@teste.com" });

            serviceProvider = services.BuildServiceProvider();
        }

        #region Mock Fake Repositories
        private void ClientReadOnlyRepositoryMock(Mock<IClientReadOnlyRepository> clientReadOnlyRepoMock)
        {
            clientReadOnlyRepoMock
                .Setup(s => s.ClientExistsAsync(Guid.Parse("4781C571-C246-470B-9CD5-13D3D169E799")))
                .ReturnsAsync(true);

            clientReadOnlyRepoMock
                .Setup(s => s.GetClientAsync(Guid.Parse("4781C571-C246-470B-9CD5-13D3D169E799")))
                .ReturnsAsync(new Client
                {
                    Id = Guid.Parse("4781C571-C246-470B-9CD5-13D3D169E799"),
                    Name = "Ze Tabajara",
                    Email = "ze@tabajara.com",
                    IsActive = true,
                    PhoneNumber = "31985632112",
                    Address = new Address
                    {
                        Street = "Rua dos testes",
                        Number = "42",
                        Neighborhood = "Bairro Novo",
                        City = "Cidade",
                        Country = "Brasil",
                        State = "MG",
                        ZipCode = "123456"
                    },
                    Subscriptions = new List<Subscription>()
                });

            clientReadOnlyRepoMock
                .Setup(s => s.GetClientWithSubscriptionsAsync(Guid.Parse("4781C571-C246-470B-9CD5-13D3D169E799")))
                .ReturnsAsync(new Client
                {
                    Name = "Ze Tabajara",
                    Email = "ze@tabajara.com",
                    IsActive = true,
                    PhoneNumber = "31985632112",
                    Address = new Address
                    {
                        Street = "Rua dos testes",
                        Number = "42",
                        Neighborhood = "Bairro Novo",
                        City = "Cidade",
                        Country = "Brasil",
                        State = "MG",
                        ZipCode = "123456"
                    },
                    Subscriptions = new List<Subscription>
                    {
                        new Subscription
                        {
                            Active = false,
                            ClientId = Guid.Parse("A2735C13-29F8-4BB4-9857-F1E07C164A68"),
                            Name = "Assinatura semanal"
                        }
                    }
                });

            clientReadOnlyRepoMock
                .Setup(s => s.GetClientWithSubscriptionsAsync(Guid.Parse("EFFE408A-5312-4094-B8CA-6B005A307FAF")))
                .ReturnsAsync(new Client
                {
                    Name = "Ze Tabajara",
                    Email = "ze@tabajara.com",
                    IsActive = true,
                    PhoneNumber = "31985632112",
                    Address = new Address
                    {
                        Street = "Rua dos testes",
                        Number = "42",
                        Neighborhood = "Bairro Novo",
                        City = "Cidade",
                        Country = "Brasil",
                        State = "MG",
                        ZipCode = "123456"
                    },
                    Subscriptions = new List<Subscription>
                    {
                                    new Subscription
                                    {
                                        Active = true,
                                        ClientId = Guid.Parse("A2735C13-29F8-4BB4-9857-F1E07C164A68"),
                                        Name = "Assinatura semanal"
                                    }
                    }
                });

        }

        private void ClientReadWriteRepositoryMock(Mock<IClientWriteOnlyRepository> clientWriteOnlyRepoMock)
        {
            clientWriteOnlyRepoMock
                .Setup(s => s.AddClientAsync(It.IsAny<Client>()))
                .Returns(Task.FromResult(new Client
                {
                    Id = clientId,
                    Name = "Ze Tabajara",
                    Email = "ze@tabajara.com",
                    IsActive = true,
                    PhoneNumber = "31985632112",
                    Address = new Address
                    {
                        Street = "Rua dos testes",
                        Number = "42",
                        Neighborhood = "Bairro Novo",
                        City = "Cidade",
                        Country = "Brasil",
                        State = "MG",
                        ZipCode = "123456"
                    }
                }));

            clientWriteOnlyRepoMock
                .Setup(s => s.EnableDisableClientAsync(clientId, true))
                .Returns(Task.FromResult(new Client
                {
                    Name = "Ze Tabajara",
                    Email = "ze@tabajara.com",
                    IsActive = true,
                    PhoneNumber = "31985632112",
                    Address = new Address
                    {
                        Street = "Rua dos testes",
                        Number = "42",
                        Neighborhood = "Bairro Novo",
                        City = "Cidade",
                        Country = "Brasil",
                        State = "MG",
                        ZipCode = "123456"
                    }
                }));

            clientWriteOnlyRepoMock
                .Setup(s => s.EnableDisableClientAsync(clientId, false))
                .Returns(Task.FromResult(new Client
                {
                    Name = "Ze Tabajara",
                    Email = "ze@tabajara.com",
                    IsActive = false,
                    PhoneNumber = "31985632112",
                    Address = new Address
                    {
                        Street = "Rua dos testes",
                        Number = "42",
                        Neighborhood = "Bairro Novo",
                        City = "Cidade",
                        Country = "Brasil",
                        State = "MG",
                        ZipCode = "123456"
                    }

                }));
        }

        private void SubscriptionWriteOnlyRepository(Mock<ISubscriptionWriteOnlyRepository> subscriptionWriteOnlyRepoMock)
        {
            subscriptionWriteOnlyRepoMock
                .Setup(s => s.AddSubscriptionAsync(It.IsAny<Subscription>()))
                .Returns(Task.FromResult(new Subscription
                {
                    ClientId = clientId,
                    Active = false,
                    Name = "Assinatura mensal",
                }));
        }

        private void SubscriptionReadOnlyRepository(Mock<ISubscriptionReadOnlyRepository> subscriptionReadOnlyRepoMock)
        {
            var subscriptions = new List<Subscription>
            {
                new Subscription
                {
                    Active = true,
                    ClientId = clientId,
                    Name = "Assinatura mensal"
                },
                new Subscription
                {
                    Active = false,
                    ClientId = clientId,
                    Name = "Assinatura anual"
                }
            };

            subscriptionReadOnlyRepoMock
                .Setup(s => s.GetClientSubscriptionsAsync(clientId, 1, 10))
                .ReturnsAsync(subscriptions);
        }
        #endregion

        [Fact]
        public async Task AddClient_With_Success()
        {
            var clientService = serviceProvider.GetService<IClientService>();

            var clientPost = new Client
            {
                Name = "Ze Tabajara",
                Email = "ze@tabajara.com",
                IsActive = true,
                PhoneNumber = "31985632112",
                Address = new Address
                {
                    Street = "Rua dos testes",
                    Number = "42",
                    Neighborhood = "Bairro Novo",
                    City = "Cidade",
                    Country = "Brasil",
                    State = "MG",
                    ZipCode = "123456"
                }
            };

            await clientService.AddClientAsync(clientPost);

            Assert.True(clientPost.Id != Guid.Empty);
        }

        [Fact]
        public async Task Add_Client_With_Invalid_Name()
        {
            var clientService = serviceProvider.GetService<IClientService>();

            var client = new Client
            {
                Name = "Lu",
                Email = "novo@teste.com",
                PhoneNumber = "31985632112",
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
            };

            await Assert.ThrowsAsync<ModelValidationException>(() => clientService.AddClientAsync(client));
        }

        [Fact]
        public async Task Get_Client_By_ClientId()
        {
            var clientService = serviceProvider.GetService<IClientService>();

            var client = await clientService.GetClientAsync(clientId);

            Assert.NotNull(client);
            Assert.IsType<Client>(client);
            Assert.Equal("Ze Tabajara", client.Name);
        }

        [Fact]
        public async Task Get_Client_By_Id_Not_Found()
        {
            var clientService = serviceProvider.GetService<IClientService>();

            clientId = Guid.Parse("5D502C13-8184-499E-8A02-A6C6A1C21189");

            var client = await clientService.GetClientAsync(clientId);

            Assert.Null(client);
        }

        [Fact]
        public async Task Add_Client_Subscription()
        {
            var subscriptionService = serviceProvider.GetService<ISubscriptionService>();

            var subscription = new Subscription
            {
                ClientId = Guid.Parse("A2735C13-29F8-4BB4-9857-F1E07C164A68"),
                Active = true,
                CreatedDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddDays(10),
                LastUpdatedDate = null
            };

            await subscriptionService.AddSubscriptionAsync(subscription);

            Assert.True(subscription.Id != Guid.Empty);
        }

        [Fact]
        public async Task Get_Client_Subscriptions()
        {
            var subscriptionService = serviceProvider.GetService<ISubscriptionService>();

            var subscriptions = await subscriptionService.GetClientSubscriptionsAsync(Guid.Parse("4781C571-C246-470B-9CD5-13D3D169E799"), 1, 10);

            Assert.True(subscriptions.Any());
        }

        [Fact]
        public async Task Throw_Exception_When_Try_Add_Two_ActiveSubscription()
        {
            var subscriptionService = serviceProvider.GetService<ISubscriptionService>();

            var subscription = new Subscription
            {
                ClientId = Guid.Parse("EFFE408A-5312-4094-B8CA-6B005A307FAF"),
                Active = true,
                CreatedDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddDays(10),
            };

            await Assert.ThrowsAsync<SubscriptionCoreException>(async () => await subscriptionService.AddSubscriptionAsync(subscription));
        }

        [Fact]
        public async Task Throw_Exception_When_Try_Exclude_Client_Not_Exists()
        {
            var clientService = serviceProvider.GetService<IClientService>();

            await Assert.ThrowsAsync<ClientCoreException>(async () => await clientService.RemoveClientAsync(Guid.Parse("5472A672-6D46-4DE4-92B1-BC61643F4938")));
        }
    }
}