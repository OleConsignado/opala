using Microsoft.Extensions.DependencyInjection;
using System;

namespace Otc.ProjectModel.Core.Application
{
    public class ApplicationConfigurationLambda
    {
        private readonly IServiceCollection services;

        public ApplicationConfigurationLambda(IServiceCollection services)
        {
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public void Configure(ApplicationConfiguration applicationConfiguration)
        {
            if (applicationConfiguration == null)
                throw new ArgumentNullException(nameof(applicationConfiguration));

            services.AddSingleton(applicationConfiguration);
        }
    }
}
