using Microsoft.Extensions.DependencyInjection;
using Otc.ProjectModel.Core.Domain.Adapters;
using System;

namespace Otc.ProjectModel.Infra.NotificationAdapter
{
    public static class NotificationAdapterServiceCollectionExtensions
    {
        public static IServiceCollection AddNotificationAdapter(this IServiceCollection services, Action<ProjectModelAdapterConfigurationLambda> configurationLambda)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddScoped<INotificationAdapter, NotificationAdapter>();

            var applicationConfigurationLambda = new ProjectModelAdapterConfigurationLambda(services);
            configurationLambda.Invoke(applicationConfigurationLambda);

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

        public void Configure(NotificationAdapterConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.AddSingleton(configuration);
        }
    }

}