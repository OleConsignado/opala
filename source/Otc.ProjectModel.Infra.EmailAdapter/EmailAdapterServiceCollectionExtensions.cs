using Microsoft.Extensions.DependencyInjection;
using Otc.ProjectModel.Core.Domain.Adapters;
using System;

namespace Otc.ProjectModel.Infra.EmailAdapter
{
    public static class EmailAdapterServiceCollectionExtensions
    {
        public static IServiceCollection AddEmailAdapter(this IServiceCollection services, Action<EmailAdapterConfigurationLambda> configurationLambda)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddScoped<IEmailAdapter, EmailAdapter>();

            var applicationConfigurationLambda = new EmailAdapterConfigurationLambda(services);
            configurationLambda.Invoke(applicationConfigurationLambda);

            return services;
        }
    }

    public class EmailAdapterConfigurationLambda
    {
        private readonly IServiceCollection services;

        public EmailAdapterConfigurationLambda(IServiceCollection services)
        {
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public void Configure(EmailAdapterConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.AddSingleton(configuration);
        }
    }

}