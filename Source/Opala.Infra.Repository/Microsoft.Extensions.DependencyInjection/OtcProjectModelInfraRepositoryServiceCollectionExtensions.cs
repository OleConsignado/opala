using Opala.Core.Domain.Repositories;
using Opala.Infra.Repository;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class OtcProjectModelInfraRepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectModelRepository(this IServiceCollection services, RepositoryConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            services.AddScoped<IClientReadOnlyRepository, ClientRepository>();
            services.AddScoped<IClientWriteOnlyRepository, ClientRepository>();
            services.AddScoped<ISubscriptionReadOnlyRepository, SubscriptionRepository>();
            services.AddScoped<ISubscriptionWriteOnlyRepository, SubscriptionRepository>();
            services.AddScoped<IPaymentReadOnlyRepository, PaymentRepository>();
            services.AddScoped<IPaymentWriteOnlyRepository, PaymentRepository>();
            services.AddSingleton(configuration);
            services.AddScoped<IDbConnection>(d =>
            {
                return new SqlConnection(configuration.SqlConnectionString);
            });

            return services;
        }
    }
}
