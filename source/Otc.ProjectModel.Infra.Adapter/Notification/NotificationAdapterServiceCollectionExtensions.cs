using Microsoft.Extensions.DependencyInjection;
using Otc.ProjectModel.Core.Domain.Adapters;
using System;

namespace Otc.ProjectModel.Infra.Adapter.Notification
{
    public static class NotificationAdapterServiceCollectionExtensions
    {
        public static IServiceCollection AddNotificationAdapter(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddScoped<INotificationAdapter, NotificationAdapter>();

            return services;
        }
    }
}