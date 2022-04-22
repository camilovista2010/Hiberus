using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DarkLoop.Azure.Functions.Authorize;
using Hiberus.Model.Models.Exceptions;
using Hiberus.Model.Models.HiberusEntity;
using Hiberus.Model.ModelsDto;
using Hiberus.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace HiberusBackend.Functions
{
    public class HiberusFunctions : BaseFunctions
    {
        private readonly IRateService RateService; 
        private readonly ITransactionService TransactionService;
        public HiberusFunctions(
            IRateService rateService,
            ITransactionService transactionService,
            IExceptionHandlerService exceptionHandler) : base(exceptionHandler)
        {
            RateService =  rateService;
            TransactionService = transactionService;
        }

        [FunctionAuthorize()]
        [FunctionName("Rate")]
        [OpenApiOperation(operationId: "Rate")]
        [OpenApiSecurity("bearer_auth", SecuritySchemeType.Http, Scheme = OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IList<Rate>))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Unauthorized, contentType: "application/json", bodyType: typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ExceptionDto))]
        public IActionResult Rate(
       [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            try
            {
                var rates = RateService.GetRates();
                return new OkObjectResult(rates);
            }
            catch (FormatException ex)
            {
                return new BadRequestObjectResult(new BusinessException(BusinessException.ERROR_PARAMETERS, ex.InnerException != null ? ex.InnerException.Message.ToString() : ex.Message.ToString()));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new BusinessException(BusinessException.UNEXPECTED_ERROR_CODE, ex.InnerException != null ? ex.InnerException.Message.ToString() : ex.Message.ToString()));
            }
        }


        [FunctionAuthorize()]
        [FunctionName("Transaction")]
        [OpenApiOperation(operationId: "Transaction" )]
        [OpenApiSecurity("bearer_auth", SecuritySchemeType.Http, Scheme = OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IList<Transaction>))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Unauthorized, contentType: "application/json", bodyType: typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ExceptionDto))]
        public IActionResult Transaction(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            try
            {
                var transactions = TransactionService.GetTransaction();
                return new OkObjectResult(transactions);
            }
            catch (FormatException ex)
            {
                return new BadRequestObjectResult(new BusinessException(BusinessException.ERROR_PARAMETERS, ex.InnerException != null ? ex.InnerException.Message.ToString() : ex.Message.ToString()));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new BusinessException(BusinessException.UNEXPECTED_ERROR_CODE, ex.InnerException != null ? ex.InnerException.Message.ToString() : ex.Message.ToString()));
            }
        }

        [FunctionAuthorize()]
        [FunctionName("TransactionBySku")]
        [OpenApiOperation(operationId: "TransactionBySku")]
        [OpenApiSecurity("bearer_auth", SecuritySchemeType.Http, Scheme = OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
        [OpenApiParameter(name: "Sku", In = ParameterLocation.Path, Required = true, Type = typeof(TransactionDto))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IList<Transaction>))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Unauthorized, contentType: "application/json", bodyType: typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ExceptionDto))]
        public IActionResult TransactionBySku(
       [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "{Sku}/transaction")] HttpRequest req ,
       string Sku)
        {
            try
            {
                var transactions = TransactionService.GetTransactionBySku(Sku);
                return new OkObjectResult(transactions);
            }
            catch (FormatException ex)
            {
                return new BadRequestObjectResult(new BusinessException(BusinessException.ERROR_PARAMETERS, ex.InnerException != null ? ex.InnerException.Message.ToString() : ex.Message.ToString()));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new BusinessException(BusinessException.UNEXPECTED_ERROR_CODE, ex.InnerException != null ? ex.InnerException.Message.ToString() : ex.Message.ToString()));
            }
        }


        [FunctionName("Token"), AllowAnonymous]
        [OpenApiOperation(operationId: "token", Description = "Get the string token data identified by Email")]
        [OpenApiParameter(name: "Email", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The Email token.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Unauthorized, contentType: "application/json", bodyType: typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ExceptionDto))]
        public async Task<ActionResult<string>> Token(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "{Email}/token")] HttpRequest req,
        string Email )
        {

            try
            { 
                string tokenAdmin = await Authorization.TokenUtils.BuildTokenForUsers(Email);
                return new OkObjectResult(tokenAdmin);
            }
            catch (FormatException ex)
            {
                return new BadRequestObjectResult(new BusinessException(BusinessException.ERROR_PARAMETERS, ex.InnerException != null ? ex.InnerException.Message.ToString() : ex.Message.ToString()));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new BusinessException(BusinessException.TOKEN_ERROR_CODE, ex.InnerException != null ? ex.InnerException.Message.ToString() : ex.Message.ToString()));
            }

        }
    }
}

