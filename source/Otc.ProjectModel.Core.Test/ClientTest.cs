using Microsoft.Extensions.DependencyInjection;
using Moq;
using Otc.ProjectModel.Core.Application;
using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Repositories;
using Otc.ProjectModel.Core.Domain.Services;
using Otc.ProjectModel.Core.Domain.ValueObjects;
using System;
using Xunit;

namespace Otc.ProjectModel.Core.Test
{
    public class ClientTest
    {
        private IServiceProvider serviceProvider;

        public ClientTest()
        {
            var services = new ServiceCollection();

            services.AddLogging();

            var clientRepository = new Mock<IClientRepository>();

            services.AddScoped(c => clientRepository.Object);
            services.AddOtcProjectModelCoreApplication();

            serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public void Add_Client_Success()
        {
            var clientService = serviceProvider.GetService<IClientService>();

            var email = new Email("luciano@teste.com");
            var address = new Address("Rua teste", "100", "Centro", "Belo Horizonte", "MG", "Brasil", "123456");

            var client = new Client("Luciano", email, address);

            clientService.AddClient(client);

            Assert.IsType<Client>(client);
        }
    }
}
