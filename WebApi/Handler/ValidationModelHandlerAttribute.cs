using FluentValidation.Results;
using Athenas.EjemploTemplateApi.Application.DataTransferObjects;
using Athenas.EjemploTemplateApi.Domain.Exceptions;
using System.Text.Json;

namespace Athenas.EjemploTemplateApi.WebApi.Handler
{
    /// <summary>
    /// Class for dispatch standar response for Bad Request
    /// </summary>
    public static class InvalidModel    
    {
        /// <summary>
        /// Dispatch response for failed validation model
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="BadRequestException"></exception>
        public static void Response(ValidationResult context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var error = new ErrorResponse
            {
                Message = "Invalid request parameters.",
                Id = Guid.NewGuid().ToString(),               
                Errors = GetErrors(context)
            };
          
            throw new BadRequestException(JsonSerializer.Serialize(error));
    
        }
        private static List<ErrorDetail> GetErrors(ValidationResult modelState)
        {
            return modelState.Errors
           .Select(x => new ErrorDetail
           {
               ErrorCode = x.ErrorCode,
               Message = x.ErrorMessage,
               Path = x.PropertyName,
            //   Url = GetUriError(key)
           }).ToList();
        }
    }

}
