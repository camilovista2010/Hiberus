using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hiberus.DataAccessLayer.DataContext;
using Hiberus.Model.MapperModels;
using System;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

[assembly: FunctionsStartup(typeof(HiberusBackend.Startup))]

namespace HiberusBackend
{
    public class Startup : FunctionsStartup
    {

        public override void Configure(IFunctionsHostBuilder builder)
        {

            builder.Services.AddMvcCore().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = Authorization.TokenUtils.tokenValidationParameters;
            });

            var configuration = GetConfigProvider(builder);
            builder.Services.AddSingleton(provider => configuration)
                .AddSettingsProviders(configuration);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            builder.Services.AddDbContext<HiberusDbContext>(
                options =>
                    options.UseNpgsql(Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_bddevHiberus"))
                 );
            #region Mappers
            builder.Services.AddSingleton(new MapperConfiguration(mc => mc.AddProfile(typeof(MapperModel))).CreateMapper());
            #endregion
            builder.Services
                .AddHttpClient()
                .AddProvidersLayer();
        }

        private IConfigurationRoot GetConfigProvider(IFunctionsHostBuilder builder)
        {
            Console.Out.WriteLine($"try: {builder.GetContext().ApplicationRootPath}");
            var config = new ConfigurationBuilder()
                .SetBasePath($"{builder.GetContext().ApplicationRootPath}")
                .AddJsonFile("local.settings.json", true)
                .AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables();
            return config.Build();
        }
    }
}
