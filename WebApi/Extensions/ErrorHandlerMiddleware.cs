using Athenas.EjemploTemplateApi.Application.DataTransferObjects;
using Athenas.EjemploTemplateApi.Domain.Exceptions;
using System.Globalization;
using System.Text.Json;

namespace Athenas.EjemploTemplateApi.WebApi.Extensions
{
    /// <summary>
    /// Controlador de errores
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        /// <param name="next"></param>
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Pasa la solicitud al siguiente controlador
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, ILogger<ErrorHandlerMiddleware> logger)
        {
            try
            {
                await _next(context).ConfigureAwait(false);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new ErrorResponse()
                {
                    Id = Guid.NewGuid().ToString(),
                    Message = error.Message
                };
                if (error is BadRequestException) 
                    responseModel = JsonSerializer.Deserialize<ErrorResponse>(error.Message);


                context.Response.StatusCode = error switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    ArgumentException => StatusCodes.Status400BadRequest,
                    BadRequestException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };
                //if you need to business code, you can to create a new switch for  responseModel.Code

                responseModel.Code = context.Response.StatusCode.ToString(CultureInfo.CurrentCulture);


                logger.LogError($"Something went wrong: {error?.ToString()}");

                await context.Response.WriteAsync(responseModel.ToString()).ConfigureAwait(false);
            }
        }
    }
}
