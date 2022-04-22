using Microsoft.Extensions.Logging;
using Hiberus.Services.Interfaces;
using System;

namespace HiberusBackend.Functions
{
    public abstract class BaseFunctions
    { 
        protected IExceptionHandlerService exceptionHandler { get; private set; }  

        protected BaseFunctions( IExceptionHandlerService exceptionHandler) 
        {  
            this.exceptionHandler = exceptionHandler ?? throw new ArgumentNullException(nameof(exceptionHandler)); 
        }
    }

}