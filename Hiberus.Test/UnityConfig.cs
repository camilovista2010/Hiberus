using AutoMapper;
using Hiberus.Services.ExternalServices;
using Hiberus.Services.Interfaces;
using Hiberus.Services.Services;
using Microsoft.Extensions.Logging;
using NUnit.Framework.Internal;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace Hiberus.Test
{
    public static class UnityConfig
    {

        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IExceptionHandlerService, ExceptionHandlerService>();
            container.RegisterType<IQuietApi, QuietApi>();
            container.RegisterType<IMapper, Mapper>();
            container.RegisterType<BaseServices, RateService>("RateService");
            container.RegisterType<BaseServices, TransactionService>("TransactionService");
            container.RegisterType<IRateService, RateService>();
            container.RegisterType<ITransactionService, TransactionService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
