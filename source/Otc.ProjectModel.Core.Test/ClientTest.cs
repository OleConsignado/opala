using Microsoft.Extensions.DependencyInjection;
using Moq;
using Otc.ProjectModel.Core.Application;
using Otc.ProjectModel.Core.Domain.Adapters;
using Otc.ProjectModel.Core.Domain.Exceptions;
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
        private IClientService clientService;
        private Address address;
        private Client client;

        public ClientTest()
        {
            var services = new ServiceCollection();

            address = new Address
            {
                Street = "Rua teste",
                Number = "100",
                Neighborhood = "Centro",
                City = "Belo Horizonte",
                State = "MG",
                Country = "Brasil",
                ZipCode = "123456"
            };

            client = new Client
            {
                Name = "Luciano",
                Email = "teste@teste.com",
                Address = address
            };

            var emailAdapter = new Mock<IEmailAdapter>();
            var clientRepository = new Mock<IClientRepository>();

            services.AddLogging();

            services.AddScoped(c => clientRepository.Object);
            services.AddScoped(c => emailAdapter.Object);

            services.AddProjectModelCoreApplication();

            serviceProvider = services.BuildServiceProvider();

            clientService = serviceProvider.GetService<IClientService>();
        }

        #region -- Client model test --
        [Fact]
        public void Add_Client_Success()
        {
            clientService.AddClient(client);

            Assert.IsType<Client>(client);
        }

        [Fact]
        public void Add_Client_With_Invalid_Name()
        {
            client = new Client
            {
                Name = "Lu",
                Email = "novo@teste.com",
                Address = address
            };

            Assert.Throws<Validations.Helpers.ValidationException>(() => clientService.AddClient(client));
        }

        [Fact]
        public void Add_Client_Subscription()
        {
            var subscription = new Subscription(null);
            var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Tabajara Company", address);

            subscription.AddPayment(payment);

            client.AddSubscription(subscription);

            Assert.True(client.HasSubscription());
        }

        [Fact]
        public void Throw_Exception_When_Try_Add_Two_Client_Subscription()
        {
            var subscription = new Subscription(null);
            var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Tabajara Company", address);

            subscription.AddPayment(payment);

            client.AddSubscription(subscription);

            subscription = new Subscription(DateTime.Now.AddDays(1));

            Assert.Throws<ClientCoreException>(() => client.AddSubscription(subscription));
        }

        [Fact]
        public void Throw_Exception_When_Try_Add_Client_Subscription_Whitou_Payment()
        {
            var subscription = new Subscription(null);

            Assert.Throws<SubscriptionCoreException>(() => client.AddSubscription(subscription));
        }
        #endregion
    }
}
