using Microsoft.Extensions.DependencyInjection;
using Moq;
using Otc.DomainBase.Exceptions;
using Opala.Core.Application;
using Opala.Core.Domain.Adapters;
using Opala.Core.Domain.Exceptions;
using Opala.Core.Domain.Models;
using Opala.Core.Domain.Repositories;
using Opala.Core.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Opala.Core.Test
{
    public class ClienteServiceTest
    {
        private readonly Guid clienteId = Guid.Parse("4781C571-C246-470B-9CD5-13D3D169E799");
        private readonly ServiceProvider serviceProvider;

        public ClienteServiceTest()
        {
            var clienteReadOnlyRepoMock = new Mock<IClienteReadOnlyRepository>();
            var clienteWriteOnlyRepoMock = new Mock<IClienteWriteOnlyRepository>();
            var assinaturaReadOnlyRepoMock = new Mock<IAssinaturaReadOnlyRepository>();
            var assinaturaWriteOnlyRepoMock = new Mock<IAssinaturaWriteOnlyRepository>();

            var emailAdapter = new Mock<IEmailAdapter>();
            var notificacaoAdapter = new Mock<INotificacaoAdapter>();

            ClienteReadOnlyRepositoryMock(clienteReadOnlyRepoMock);
            ClienteReadWriteRepositoryMock(clienteWriteOnlyRepoMock);

            AssinaturaWriteOnlyRepository(assinaturaWriteOnlyRepoMock);
            AssinaturaReadOnlyRepository(assinaturaReadOnlyRepoMock);

            IServiceCollection services = new ServiceCollection();

            services.AddLogging();
            services.AddTransient(t => clienteReadOnlyRepoMock.Object);

            services.AddScoped(c => emailAdapter.Object);
            services.AddScoped(c => notificacaoAdapter.Object);

            services.AddTransient(t => clienteWriteOnlyRepoMock.Object);
            services.AddTransient(t => clienteWriteOnlyRepoMock.Object);
            services.AddTransient(t => assinaturaReadOnlyRepoMock.Object);
            services.AddTransient(t => assinaturaWriteOnlyRepoMock.Object);

            services.AddProjectModelCoreApplication(new ApplicationConfiguration { EmailFrom = "meu.teste@teste.com" });

            serviceProvider = services.BuildServiceProvider();
        }

        #region Mock Fake Repositories
        private void ClienteReadOnlyRepositoryMock(Mock<IClienteReadOnlyRepository> clientReadOnlyRepoMock)
        {
            clientReadOnlyRepoMock
                .Setup(s => s.ClienteExisteAsync(Guid.Parse("4781C571-C246-470B-9CD5-13D3D169E799")))
                .ReturnsAsync(true);

            clientReadOnlyRepoMock
                .Setup(s => s.RetornaClienteAsync(Guid.Parse("4781C571-C246-470B-9CD5-13D3D169E799")))
                .ReturnsAsync(new Cliente
                {
                    Id = Guid.Parse("4781C571-C246-470B-9CD5-13D3D169E799"),
                    Nome = "Ze Tabajara",
                    Email = "ze@tabajara.com",
                    Ativo = true,
                    Telefone = "31985632112",
                    Endereco = new Endereco
                    {
                        Rua = "Rua dos testes",
                        Numero = "42",
                        Bairro = "Bairro Novo",
                        Cidade = "Cidade",
                        Pais = "Brasil",
                        Estado = "MG",
                        Cep = "123456"
                    },
                    Assinaturas = new List<Assinatura>()
                });

            clientReadOnlyRepoMock
                .Setup(s => s.RetornaClienteSemAssinaturaAsync(Guid.Parse("4781C571-C246-470B-9CD5-13D3D169E799")))
                .ReturnsAsync(new Cliente
                {
                    Nome = "Ze Tabajara",
                    Email = "ze@tabajara.com",
                    Ativo = true,
                    Telefone = "31985632112",
                    Endereco = new Endereco
                    {
                        Rua = "Rua dos testes",
                        Numero = "42",
                        Bairro = "Bairro Novo",
                        Cidade = "Cidade",
                        Pais = "Brasil",
                        Estado = "MG",
                        Cep = "123456"
                    },
                    Assinaturas = new List<Assinatura>
                    {
                        new Assinatura
                        {
                            Ativa = false,
                            ClienteId = Guid.Parse("A2735C13-29F8-4BB4-9857-F1E07C164A68"),
                            Nome = "Assinatura semanal"
                        }
                    }
                });

            clientReadOnlyRepoMock
                .Setup(s => s.RetornaClienteSemAssinaturaAsync(Guid.Parse("EFFE408A-5312-4094-B8CA-6B005A307FAF")))
                .ReturnsAsync(new Cliente
                {
                    Nome = "Ze Tabajara",
                    Email = "ze@tabajara.com",
                    Ativo = true,
                    Telefone = "31985632112",
                    Endereco = new Endereco
                    {
                        Rua = "Rua dos testes",
                        Numero = "42",
                        Bairro = "Bairro Novo",
                        Cidade = "Cidade",
                        Pais = "Brasil",
                        Estado = "MG",
                        Cep = "123456"
                    },
                    Assinaturas = new List<Assinatura>
                    {
                                    new Assinatura
                                    {
                                        Ativa = true,
                                        ClienteId = Guid.Parse("A2735C13-29F8-4BB4-9857-F1E07C164A68"),
                                        Nome = "Assinatura semanal"
                                    }
                    }
                });

        }

        private void ClienteReadWriteRepositoryMock(Mock<IClienteWriteOnlyRepository> clientWriteOnlyRepoMock)
        {
            clientWriteOnlyRepoMock
                .Setup(s => s.IncluiClienteAsync(It.IsAny<Cliente>()))
                .Returns(Task.FromResult(new Cliente
                {
                    Id = clienteId,
                    Nome = "Ze Tabajara",
                    Email = "ze@tabajara.com",
                    Ativo = true,
                    Telefone = "31985632112",
                    Endereco = new Endereco
                    {
                        Rua = "Rua dos testes",
                        Numero = "42",
                        Bairro = "Bairro Novo",
                        Cidade = "Cidade",
                        Pais = "Brasil",
                        Estado = "MG",
                        Cep = "123456"
                    }
                }));

            clientWriteOnlyRepoMock
                .Setup(s => s.AtivaDesativaClienteAsync(clienteId, true))
                .Returns(Task.FromResult(new Cliente
                {
                    Nome = "Ze Tabajara",
                    Email = "ze@tabajara.com",
                    Ativo = true,
                    Telefone = "31985632112",
                    Endereco = new Endereco
                    {
                        Rua = "Rua dos testes",
                        Numero = "42",
                        Bairro = "Bairro Novo",
                        Cidade = "Cidade",
                        Pais = "Brasil",
                        Estado = "MG",
                        Cep = "123456"
                    }
                }));

            clientWriteOnlyRepoMock
                .Setup(s => s.AtivaDesativaClienteAsync(clienteId, false))
                .Returns(Task.FromResult(new Cliente
                {
                    Nome = "Ze Tabajara",
                    Email = "ze@tabajara.com",
                    Ativo = false,
                    Telefone = "31985632112",
                    Endereco = new Endereco
                    {
                        Rua = "Rua dos testes",
                        Numero = "42",
                        Bairro = "Bairro Novo",
                        Cidade = "Cidade",
                        Pais = "Brasil",
                        Estado = "MG",
                        Cep = "123456"
                    }

                }));
        }

        private void AssinaturaWriteOnlyRepository(Mock<IAssinaturaWriteOnlyRepository> subscriptionWriteOnlyRepoMock)
        {
            subscriptionWriteOnlyRepoMock
                .Setup(s => s.IncluiAssinaturaAsync(It.IsAny<Assinatura>()))
                .Returns(Task.FromResult(new Assinatura
                {
                    ClienteId = clienteId,
                    Ativa = false,
                    Nome = "Assinatura mensal",
                }));
        }

        private void AssinaturaReadOnlyRepository(Mock<IAssinaturaReadOnlyRepository> subscriptionReadOnlyRepoMock)
        {
            var subscriptions = new List<Assinatura>
            {
                new Assinatura
                {
                    Ativa = true,
                    ClienteId = clienteId,
                    Nome = "Assinatura mensal"
                },
                new Assinatura
                {
                    Ativa = false,
                    ClienteId = clienteId,
                    Nome = "Assinatura anual"
                }
            };

            subscriptionReadOnlyRepoMock
                .Setup(s => s.RetornaAssinaturasClienteAsync(clienteId, 1, 10))
                .ReturnsAsync(subscriptions);
        }
        #endregion

        [Fact]
        public async Task AddClient_With_Success()
        {
            var clientService = serviceProvider.GetService<IClienteService>();

            var clientPost = new Cliente
            {
                Nome = "Ze Tabajara",
                Email = "ze@tabajara.com",
                Ativo = true,
                Telefone = "31985632112",
                Endereco = new Endereco
                {
                    Rua = "Rua dos testes",
                    Numero = "42",
                    Bairro = "Bairro Novo",
                    Cidade = "Cidade",
                    Pais = "Brasil",
                    Estado = "MG",
                    Cep = "123456"
                }
            };

            await clientService.IncluiClienteAsync(clientPost);

            Assert.True(clientPost.Id != Guid.Empty);
        }

        [Fact]
        public async Task Add_Client_With_Invalid_Name()
        {
            var clientService = serviceProvider.GetService<IClienteService>();

            var client = new Cliente
            {
                Nome = "Ze",
                Email = "ze@tabajara.com",
                Ativo = true,
                Telefone = "31985632112",
                Endereco = new Endereco
                {
                    Rua = "Rua dos testes",
                    Numero = "42",
                    Bairro = "Bairro Novo",
                    Cidade = "Cidade",
                    Pais = "Brasil",
                    Estado = "MG",
                    Cep = "123456"
                }
            };

            await Assert.ThrowsAnyAsync<ModelValidationException>(() => clientService.IncluiClienteAsync(client));
        }

        [Fact]
        public async Task Get_Client_By_ClientId()
        {
            var clientService = serviceProvider.GetService<IClienteService>();

            var client = await clientService.RetornaClienteAsync(clienteId);

            Assert.NotNull(client);
            Assert.IsType<Cliente>(client);
            Assert.Equal("Ze Tabajara", client.Nome);
        }

        [Fact]
        public async Task Get_Client_By_Id_Not_Found()
        {
            var clientService = serviceProvider.GetService<IClienteService>();

            var client = await clientService.RetornaClienteAsync(Guid.Parse("5D502C13-8184-499E-8A02-A6C6A1C21189"));

            Assert.Null(client);
        }

        [Fact]
        public async Task Add_Client_Subscription()
        {
            var subscriptionService = serviceProvider.GetService<IAssinaturaService>();

            var subscription = new Assinatura
            {
                ClienteId = Guid.Parse("4781C571-C246-470B-9CD5-13D3D169E799"),
                Ativa = true,
                DataCriacao = DateTime.Now,
                DataExpiracao = DateTime.Now.AddDays(10),
                DataUltimaAtualizacao = null
            };

            await subscriptionService.IncluiAssinaturaAsync(subscription);

            Assert.True(subscription.Id != Guid.Empty);
        }

        [Fact]
        public async Task Get_Client_Subscriptions()
        {
            var subscriptionService = serviceProvider.GetService<IAssinaturaService>();

            var subscriptions = await subscriptionService.ListaAssinaturasClienteAsync(Guid.Parse("4781C571-C246-470B-9CD5-13D3D169E799"), 1, 10);

            Assert.True(subscriptions.Any());
        }

        [Fact]
        public async Task Throw_Exception_When_Try_Add_Two_ActiveSubscription()
        {
            var subscriptionService = serviceProvider.GetService<IAssinaturaService>();

            var subscription = new Assinatura
            {
                ClienteId = Guid.Parse("EFFE408A-5312-4094-B8CA-6B005A307FAF"),
                Ativa = true,
                DataCriacao = DateTime.Now,
                DataExpiracao = DateTime.Now.AddDays(10),
            };

            await Assert.ThrowsAsync<AssinaturaCoreException>(async () => await subscriptionService.IncluiAssinaturaAsync(subscription));
        }

        [Fact]
        public async Task Throw_Exception_When_Try_Exclude_Client_Not_Exists()
        {
            var clientService = serviceProvider.GetService<IClienteService>();

            await Assert.ThrowsAsync<ClienteCoreException>(async () => await clientService.ExcluiClienteAsync(Guid.Parse("5472A672-6D46-4DE4-92B1-BC61643F4938")));
        }
    }
}