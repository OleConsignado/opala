using Microsoft.Extensions.DependencyInjection;
using Otc.ProjectModel.Core.Domain.Repositories;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Otc.ProjectModel.Infra.Repository
{
    public static class OtcProjectModelInfraRepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectModelRepository(this IServiceCollection services, Action<ProjectModelRepositorConfigurationLambda> configurationLambda)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configurationLambda == null)
            {
                throw new ArgumentNullException(nameof(configurationLambda));
            }

            services.AddScoped<IClientReadOnlyRepository, ClientRepository>();
            services.AddScoped<IClientWriteOnlyRepository, ClientRepository>();

            var dbConfigurationLambda = new ProjectModelRepositorConfigurationLambda(services);

            configurationLambda.Invoke(dbConfigurationLambda);

            return services;
        }
    }

    public class ProjectModelRepositorConfigurationLambda
    {
        private readonly IServiceCollection services;

        public ProjectModelRepositorConfigurationLambda(IServiceCollection services)
        {
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public void Configure(ProjectModelRepositoryConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.AddSingleton(configuration);

            services.AddScoped<IDbConnection>(d =>
            {
                return new SqlConnection(configuration.SqlConnectionString);
            });
        }
    }
}
