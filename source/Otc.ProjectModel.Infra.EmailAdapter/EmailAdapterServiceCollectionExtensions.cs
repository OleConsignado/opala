using Microsoft.Extensions.DependencyInjection;
using Otc.ProjectModel.Core.Domain.Adapters;
using System;

namespace Otc.ProjectModel.Infra.EmailAdapter
{
    public static class EmailAdapterServiceCollectionExtensions
    {
        public static IServiceCollection AddEmailAdapter(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddScoped<IEmailAdapter, EmailAdapter>();

            return services;
        }
    }
}