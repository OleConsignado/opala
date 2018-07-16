using Microsoft.Extensions.DependencyInjection;
using Otc.ProjectModel.Core.Domain.Adapters;
using System;

namespace Otc.ProjectModel.Infra.Adapter
{
    public static class OtcProjectModelInfraAdapterServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectModelAdapter(this IServiceCollection services, Action<ProjectModelAdapterConfigurationLambda> configurationLambda)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configurationLambda == null)
            {
                throw new ArgumentNullException(nameof(configurationLambda));
            }

            services.AddScoped<IEmailAdapter, EmailAdapter>();

            return services;
        }
    }

    public class ProjectModelAdapterConfigurationLambda
    {
        private readonly IServiceCollection services;

        public ProjectModelAdapterConfigurationLambda(IServiceCollection services)
        {
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public void Configure(ProjectModelAdapterConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.AddSingleton(configuration);
        }
    }
}
