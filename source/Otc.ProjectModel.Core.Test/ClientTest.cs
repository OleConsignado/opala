using Microsoft.Extensions.DependencyInjection;
using Moq;
using Otc.ProjectModel.Core.Application;
using Otc.ProjectModel.Core.Domain.Adapters;
using Otc.ProjectModel.Core.Domain.Models;
using Otc.ProjectModel.Core.Domain.Repositories;
using Otc.ProjectModel.Core.Domain.Services;
using Otc.ProjectModel.Core.Test.Mock;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Otc.ProjectModel.Core.Test
{
    public class ClientTest
    {
        private IServiceProvider serviceProvider;
        private IClientService clientService;
        private ISubscriptionService subscriptionService;
        private IPaymentService paymentService;

        private Address address;
        private Client client;
        private Guid clientId = Guid.Parse("5D502C13-8184-499E-8A02-A6C6A1C21188");

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

            services.AddLogging();

            services.AddScoped<IClientReadOnlyRepository, FakeClientRepository>();
            services.AddScoped<IClientWriteOnlyRepository, FakeClientRepository>();
            services.AddScoped<IEmailAdapter, FakeEmailAdapter>();

            services.AddScoped(c => emailAdapter.Object);

            services.AddProjectModelCoreApplication(c => c.Configure(new ApplicationConfiguration { EmailFrom = "meu.teste@teste.com" }));

            serviceProvider = services.BuildServiceProvider();

            clientService = serviceProvider.GetService<IClientService>();
            subscriptionService = serviceProvider.GetService<ISubscriptionService>();
            paymentService = serviceProvider.GetService<IPaymentService>();

        }

        #region -- Client --
        [Fact]
        public void Add_Client_Success()
        {
            clientService.AddClientAsync(client);

            Assert.IsType<Client>(client);
        }

        [Fact]
        public async Task Add_Client_With_Invalid_Name()
        {
            client = new Client
            {
                Name = "Lu",
                Email = "novo@teste.com",
                Address = address
            };

            await Assert.ThrowsAsync<Validations.Helpers.ValidationException>(() => clientService.AddClientAsync(client));
        }

        [Fact]
        public void Add_Client_Subscription()
        {
            var subscription = new Subscription
            {
                Active = true,
                CreateDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddDays(10),
                LastUpdateDate = null
            };

            var payment = new PayPalPayment
            {
                TransactionCode = "12345678",
                PaidDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddDays(5),
                Total = 10,
                TotalPaid = 10,
                Payer = "Tabajara Company",
                Address = address
            };

            subscription.Payments.Add(payment);

            subscriptionService.AddSubscriptionAsync(client, subscription);

            Assert.True(client.HasSubscription());
        }

        //[Fact]
        //public void Throw_Exception_When_Try_Add_Two_Client_Subscription()
        //{
        //    var subscription = new Subscription
        //    {
        //        Active = true,
        //        CreateDate = DateTime.Now,
        //        ExpireDate = DateTime.Now.AddDays(10),
        //    };


        //    subscriptionService.AddSubscription(client, subscription);

        //    subscription = new Subscription
        //    {
        //        Active = true,
        //        CreateDate = DateTime.Now,
        //        ExpireDate = DateTime.Now.AddDays(1),
        //    };

        //    Assert.Throws<ClientCoreException>(() => subscriptionService.AddSubscription(client, subscription));
        //}

        [Fact]
        public async Task Get_Client_By_ClientId()
        {
            var client = await clientService.GetClientAsync(clientId);

            Assert.NotNull(client);
            Assert.IsType<Client>(client);
            Assert.Equal("Luciano", client.Name);
        }

        [Fact]
        public async Task Get_Client_By_Id_Not_Found()
        {
            clientId = Guid.Parse("5D502C13-8184-499E-8A02-A6C6A1C21189");

            var client = await clientService.GetClientAsync(clientId);

            Assert.Null(client);
        }

        #endregion

    }
}
