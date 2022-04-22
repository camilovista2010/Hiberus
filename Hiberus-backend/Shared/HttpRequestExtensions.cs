using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiberusBackend.Shared
{
    public static class HttpRequestExtensions
    {
        // El método genérico GetJsonBody()maneja convenientemente tanto la deserialización como la validación.
        /// <summary>
        /// Retorna deserealizaddo el request body, adicional valida si el modelo cumple con las validaciones.
        /// </summary>
        /// <typeparam name="T">T Sera el tipo en el que se debe deserealizar</typeparam>
        /// <typeparam name="V">
        /// V Sera El modelo con la validacion de la libreria FluentValidation, estas se encontraran en el proyecto Hiberus.Model.ValidationModels .
        /// </typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<ValidatableRequest<T>> GetJsonBody<T, V>(this HttpRequest request) where V : AbstractValidator<T>, new()
        {
            var resultBody = await request.GetJsonBody<T>();
            var validator = new V();
            var validationResult = validator.Validate(resultBody);

            if (!validationResult.IsValid)
            {
                return new ValidatableRequest<T>
                {
                    Value = resultBody,
                    IsValid = false,
                    Errors = validationResult.Errors
                };
            }

            return new ValidatableRequest<T>
            {
                Value = resultBody,
                IsValid = true
            };
        }

        /// <summary>
        /// Retorna el body de la solicitud deserealizado
        /// </summary>
        /// <typeparam name="T">T debe ser el tipo de modelo en el que se deserealizara</typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<T> GetJsonBody<T>(this HttpRequest request)
        {
            var requestBody = await request.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(requestBody);
        }
    }
    public static class ValidationExtensions
    {
        /// <summary>
        /// Creates a <see cref="BadRequestObjectResult"/> contiene una coleccion de los errores
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static BadRequestObjectResult ToBadRequest<T>(this ValidatableRequest<T> request)
        {
            return new BadRequestObjectResult(request.Errors.Select(e => new
            {
                Campo = e.PropertyName,
                Error = e.ErrorMessage
            }).ToList());

        }
    }
    public class ValidatableRequest<T>
    {
        /// <summary>
        /// Almacena la deserelizacion del el valor del body.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Indica si el valor deserializado era válido o no.
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Colección de errores de la validación
        /// </summary>
        public IList<ValidationFailure> Errors { get; set; }
    }
}