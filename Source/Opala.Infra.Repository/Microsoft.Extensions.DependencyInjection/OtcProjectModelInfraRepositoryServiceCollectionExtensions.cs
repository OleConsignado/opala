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

            services.AddScoped<IClienteReadOnlyRepository, ClienteRepository>();
            services.AddScoped<IClienteWriteOnlyRepository, ClienteRepository>();
            services.AddScoped<IAssinaturaReadOnlyRepository, AssinaturaRepository>();
            services.AddScoped<IAssinaturaWriteOnlyRepository, AssinaturaRepository>();
            services.AddScoped<IPagamentoReadOnlyRepository, PagamentoRepository>();
            services.AddScoped<IPagamentoWriteOnlyRepository, PagamentoRepository>();
            services.AddSingleton(configuration);
            services.AddScoped<IDbConnection>(d =>
            {
                return new SqlConnection(configuration.SqlConnectionString);
            });

            return services;
        }
    }
}
