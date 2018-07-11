using Microsoft.Extensions.DependencyInjection;
using Otc.ProjectModel.Core.Application.Services;
using Otc.ProjectModel.Core.Domain.Services;
using System;

namespace Otc.ProjectModel.Core.Application
{
    public static class OtcProjectModelCoreApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddOtcProjectModelCoreApplication(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddScoped<IClientService, ClientService>();

            return services;
        }
    }
}
