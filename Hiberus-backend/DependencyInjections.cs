using Microsoft.Extensions.DependencyInjection;
using Hiberus.DataAccessLayer.Dal;
using Hiberus.DataAccessLayer.Dal.Interfaces;
using Hiberus.Services.Interfaces;
using Hiberus.Services.Services;
using Hiberus.Services.ExternalServices;

namespace HiberusBackend
{
    internal static class DependencyInjections
    {
        public static IServiceCollection AddProvidersLayer(this IServiceCollection services)
        {
            
            #region Services
            services.AddTransient<IRateService, RateService>();
            services.AddTransient<IExceptionHandlerService, ExceptionHandlerService>();
            services.AddTransient<ITransactionService, TransactionService>(); 
            #endregion
            #region Services External 
            services.AddTransient<IQuietApi, QuietApi>();
            #endregion
            #region DataAccessLayer
            services.AddTransient<IRateDal,RateDal>();
            services.AddTransient<ITransactionDal,TransactionDal>();
            #endregion
            return services;
        }
    }
}
