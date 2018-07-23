using Microsoft.Extensions.DependencyInjection;
using Otc.ProjectModel.Core.Application.Services;
using Otc.ProjectModel.Core.Domain.Services;
using System;

namespace Otc.ProjectModel.Core.Application
{
    public static class OtcProjectModelCoreApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectModelCoreApplication(this IServiceCollection services, Action<ApplicationConfigurationLambda> configurationLambda)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();

            var applicationConfigurationLambda = new ApplicationConfigurationLambda(services);
            configurationLambda.Invoke(applicationConfigurationLambda);

            return services;
        }
    }
}
