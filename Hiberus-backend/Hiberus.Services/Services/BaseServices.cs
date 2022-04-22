using AutoMapper;
using Microsoft.Extensions.Logging;
using Hiberus.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hiberus.Services.ExternalServices;

namespace Hiberus.Services.Services
{
    public abstract class BaseServices
    {
        protected ILogger<BaseServices> Logger { get; private set; }
        protected IExceptionHandlerService ExceptionHandler { get; private set; }
        protected IMapper Mapper { get; private set; } 
        protected IQuietApi QuietApi { get; private set; }

        protected BaseServices(ILogger<BaseServices> logger, IExceptionHandlerService exceptionHandler, IMapper mapper , IQuietApi quietApi)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.ExceptionHandler = exceptionHandler ?? throw new ArgumentNullException(nameof(exceptionHandler));
            this.Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.QuietApi = quietApi ?? throw new ArgumentNullException(nameof(quietApi));
        }
    }
}
