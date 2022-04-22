using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hiberus.Model.Models.GloblalSettings;

namespace HiberusBackend
{
    internal static class StartupSettings
    {
        public static IServiceCollection AddSettingsProviders(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GlobalSettings>(configuration);
            services.Configure<HealthModel>(configuration.GetSection(HealthModel.Position));
            return services;
        }
    }
}
