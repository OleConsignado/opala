using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Otc.Mvc.Filters;
using Otc.SwaggerSchemaFiltering;
using Serilog;
using Serilog.Formatting.Json;
using Swashbuckle.AspNetCore.Swagger;
using Otc.ProjectModel.Core.Application;
using Otc.ProjectModel.Infra.Repository;
using Graceterm;
using Otc.Extensions.Configuration;
using Otc.ProjectModel.Infra.EmailAdapter;
using Otc.ProjectModel.Infra.NotificationAdapter;
using Otc.RequestTracking.AspNetCore;

namespace Otc.ProjectModel.WebApi
{
    public class Startup
    {
        protected static string GetXmlCommentsPath() => Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, $"{PlatformServices.Default.Application.ApplicationName}.xml");

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add<ExceptionFilter>();
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddExceptionHandling();

            services.AddLogging(configure =>
            {
                configure.ClearProviders();

                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Async(a => a.Console(new JsonFormatter()))
                    .Enrich.FromLogContext()
                    .ReadFrom.Configuration(Configuration)
                    .CreateLogger();

                configure.AddSerilog();
                configure.AddDebug();
            });

            services.AddSwaggerGen(c =>
            {
                c.SchemaFilter<SwaggerExcludeFilter>();
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = ApplicationProperties.Name,
                        Version = "v1",
                        Description = ApplicationProperties.Description
                    });
                c.IncludeXmlComments(GetXmlCommentsPath());
            });

            services.AddProjectModelCoreApplication(c => c.Configure(Configuration.SafeGet<ApplicationConfiguration>()));

            services.AddProjectModelRepository(c => c.Configure(Configuration.SafeGet<ProjectModelRepositoryConfiguration>()));

            services.AddEmailAdapter(c => c.Configure(Configuration.SafeGet<EmailAdapterConfiguration>()));

            services.AddNotificationAdapter(c => c.Configure(Configuration.SafeGet<NotificationAdapterConfiguration>()));

            services.AddRequestTracking(requestTracker =>
            {
                requestTracker.Configure(Configuration.SafeGet<RequestTrackerConfiguration>());
            });

            // Inicializa os mapeamentos feito com o AutoMapper
            Mappings.Initialize();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseRequestTracking();

            app.UseGraceterm(options =>
            {
                options.IgnorePath(ApplicationProperties.HealthzPath);
            });

            app.UseMvc();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", ApplicationProperties.Name);
            });
        }
    }
}