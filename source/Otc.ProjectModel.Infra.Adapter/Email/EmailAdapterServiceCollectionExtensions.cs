using Microsoft.Extensions.DependencyInjection;
using Otc.ProjectModel.Core.Domain.Adapters;
using System;

namespace Otc.ProjectModel.Infra.Adapter.Email
{
    public static class EmailAdapterServiceCollectionExtensions
    {
        public static IServiceCollection AddNotificacaoWsAdapter(this IServiceCollection services)
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