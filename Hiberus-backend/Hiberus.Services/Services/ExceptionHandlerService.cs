using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Hiberus.Model.Models.Exceptions;
using Hiberus.Services.Interfaces;

namespace Hiberus.Services.Services
{
    public class ExceptionHandlerService : IExceptionHandlerService
    {

        private const string UNEXPECTED_ERROR_CODE = "UNEXPECTED_ERROR_CODE";
        private const string UNEXPECTED_ERROR_MSG = "Unexpected error";
        private readonly ILogger<ExceptionHandlerService> logger;

        public ExceptionHandlerService(ILogger<ExceptionHandlerService> _logger)
        {
            logger = _logger;
        }

        public IActionResult HandleException(Exception exception)
        {
            ObjectResult result = null;
            ErrorResponse errorResponse = new ErrorResponse();

            logger.LogError(exception.ToString());

            switch (exception)
            {
                case BusinessException ex:
                    errorResponse.code = ex.code;
                    errorResponse.message = ex.Message; 
                    result = new ObjectResult(errorResponse);
                    if (ex.code.Equals(BusinessException.RESOURCE_NOT_FOUND))
                    { 
                        result.StatusCode = StatusCodes.Status404NotFound;
                    }
                    if (ex.code.Equals(BusinessException.ERROR_PARAMETERS)) 
                    { 
                        result.StatusCode = StatusCodes.Status400BadRequest;
                    }
                    if (ex.code.Equals(BusinessException.UNEXPECTED_ERROR_CODE))
                    { 
                        result.StatusCode = StatusCodes.Status500InternalServerError;
                    }
                    if (ex.code.Equals(BusinessException.TOKEN_ERROR_CODE))
                    { 
                        result.StatusCode = StatusCodes.Status401Unauthorized;
                    }
                    if (ex.code.Equals(BusinessException.FORBIDDEN_RESOURCE_CODE))
                    { 
                        result.StatusCode = StatusCodes.Status403Forbidden;
                    }
                    break;
                default:
                    errorResponse.code = UNEXPECTED_ERROR_CODE;
                    errorResponse.message = UNEXPECTED_ERROR_MSG;

                    result = new ObjectResult(errorResponse);
                    result.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }
            return result;
        }

    }
}
