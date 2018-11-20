using Opala.Core.Domain.Adapters;
using Opala.Infra.NotificationAdapter;
using System;
using Opala.Infra.NotificationAdapter.Clients;
using Otc.Networking.Http.Client.Abstractions;
using Refit;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class NotificationAdapterServiceCollectionExtensions
    {
        public static IServiceCollection AddNotificationAdapter(this IServiceCollection services, NotificacaoAdapterConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<INotificacaoAdapter, NotificacaoAdapter>();

            services.AddScoped(serviceProvider =>
            {
                var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateHttpClient();
                httpClient.BaseAddress = new Uri(configuration.NotificacaoUrl);

                return RestService.For<INotificacaoClient>(httpClient);
            });

            services.AddSingleton(configuration);

            return services;
        }
    }
}