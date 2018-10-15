using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Opala.Core.Application;
using Opala.Infra.Repository;
using Otc.Extensions.Configuration;
using Opala.Infra.EmailAdapter;
using Opala.Infra.NotificationAdapter;
using Otc.AspNetCore.ApiBoot;
using Otc.RequestTracking.AspNetCore;
using AutoMapper;

namespace Opala.WebApi
{
    public class Startup : ApiBootStartup
    {
        protected override ApiMetadata ApiMetadata => new ApiMetadata()
        {
            Name = ApplicationProperties.Name,
            Description = ApplicationProperties.Description,
            DefaultApiVersion = "1.0"
        };
        protected override void ConfigureApiServices(IServiceCollection services)
        {
            services.AddProjectModelCoreApplication(Configuration.SafeGet<ApplicationConfiguration>());

            services.AddProjectModelRepository(Configuration.SafeGet<RepositoryConfiguration>());
            services.AddEmailAdapter(Configuration.SafeGet<EmailAdapterConfiguration>());
            services.AddNotificationAdapter(Configuration.SafeGet<NotificationAdapterConfiguration>());
            services.AddRequestTracking(requestTracker =>
            {
                requestTracker.Configure(Configuration.SafeGet<RequestTrackerConfiguration>());
            });

        }

        public Startup(IConfiguration configuration) : base(configuration)
        {
            // Inicializa os mapeamentos feito com o AutoMapper
            Mapper.Initialize(m => m.AddProfile<WebApiAutoMapperProfile>());
        }
    }
}