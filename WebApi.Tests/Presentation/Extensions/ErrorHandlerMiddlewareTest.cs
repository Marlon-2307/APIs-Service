using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Athenas.EjemploTemplateApi.Application.DataTransferObjects;
using Athenas.EjemploTemplateApi.Domain.Exceptions;
using Athenas.EjemploTemplateApi.WebApi.Extensions;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace WebApi.Test.Extensions
{
    public  class ErrorHandlerMiddlewareTest
    {
		[Fact]
		public async Task When_NotFoundException_Returns404StatusCode()
		{
			var _loggerMock = new Mock<ILogger<ErrorHandlerMiddleware>>();
			//arrange
			var expectedException = new CompanyNotFoundException();
			RequestDelegate mockNextMiddleware = (HttpContext) =>
			{
				return Task.FromException(expectedException);
			};
			var httpContext = new DefaultHttpContext();

			var exceptionHandlingMiddleware = new ErrorHandlerMiddleware(mockNextMiddleware);

			//act
			await exceptionHandlingMiddleware.Invoke(httpContext,_loggerMock.Object);

			//assert
			Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)httpContext.Response.StatusCode);
		}
		[Fact]
		public async Task When_BadRequestException_Returns404StatusCode()
		{
			var _loggerMock = new Mock<ILogger<ErrorHandlerMiddleware>>();
			//arrange
			var error = new ErrorResponse
			{
				Message = "Invalid request parameters.",
				Id = Guid.NewGuid().ToString()				
			};
					   
			var expectedException = new BadRequestException(JsonSerializer.Serialize(error));
			
			RequestDelegate mockNextMiddleware = (HttpContext) =>
			{
				return Task.FromException(expectedException);
			};

			var httpContext = new DefaultHttpContext();

			var exceptionHandlingMiddleware = new ErrorHandlerMiddleware(mockNextMiddleware);

			//act
			await exceptionHandlingMiddleware.Invoke(httpContext,_loggerMock.Object);

			//assert
			Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)httpContext.Response.StatusCode);
		}
		[Fact]
		public async Task When_UnknownException_Returns500StatusCode()
		{
			var _loggerMock = new Mock<ILogger<ErrorHandlerMiddleware>>();
			//arrange
			var expectedException = new Exception();
			RequestDelegate mockNextMiddleware = (HttpContext) =>
			{
				return Task.FromException(expectedException);
			};
			var httpContext = new DefaultHttpContext();

			var exceptionHandlingMiddleware = new ErrorHandlerMiddleware(mockNextMiddleware);

			//act
			await exceptionHandlingMiddleware.Invoke(httpContext,_loggerMock.Object);

			//assert
			Assert.Equal(HttpStatusCode.InternalServerError, (HttpStatusCode)httpContext.Response.StatusCode);
		}
	}
}
