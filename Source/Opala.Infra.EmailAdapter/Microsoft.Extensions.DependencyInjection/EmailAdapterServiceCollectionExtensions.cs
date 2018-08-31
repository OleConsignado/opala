using Opala.Core.Domain.Adapters;
using Opala.Infra.EmailAdapter;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EmailAdapterServiceCollectionExtensions
    {
        public static IServiceCollection AddEmailAdapter(this IServiceCollection services, EmailAdapterConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<IEmailAdapter, EmailAdapter>();
            services.AddSingleton(configuration);

            return services;
        }
    }
}