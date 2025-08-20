using API.Middlewares;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text;

namespace API.Tests.Middlewares
{
    public class ErrorHandlingMiddlewareTests
    {
        private DefaultHttpContext CreateHttpContext()
        {
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            return context;
        }

        private string GetResponseBody(HttpContext context)
        {
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(context.Response.Body, Encoding.UTF8);
            return reader.ReadToEnd();
        }

        [Fact]
        public async Task InvokeAsync_Returns404_WhenNotFoundExceptionThrown()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var context = CreateHttpContext();
            RequestDelegate next = _ => throw new NotFoundException("Not found", "ResourceId");

            // Act
            await middleware.InvokeAsync(context, next);

            // Assert
            Assert.Equal(StatusCodes.Status404NotFound, context.Response.StatusCode);
            Assert.Equal("Not found", GetResponseBody(context));
        }

        [Fact]
        public async Task InvokeAsync_Returns403_WhenAccessForbiddenExceptionThrown()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var context = CreateHttpContext();
            RequestDelegate next = _ => throw new AccessForbiddenException();

            // Act
            await middleware.InvokeAsync(context, next);

            // Assert
            Assert.Equal(StatusCodes.Status403Forbidden, context.Response.StatusCode);
            Assert.Equal("Forbidden", GetResponseBody(context));
        }

        [Fact]
        public async Task InvokeAsync_Returns500_WhenGeneralExceptionThrown()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var context = CreateHttpContext();
            RequestDelegate next = _ => throw new Exception("General error");

            // Act
            await middleware.InvokeAsync(context, next);

            // Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
            Assert.Equal("Something went wrong.", GetResponseBody(context));
        }

        [Fact]
        public async Task InvokeAsync_CallsNext_WhenNoException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var context = CreateHttpContext();
            var called = false;
            RequestDelegate next = ctx => { called = true; return Task.CompletedTask; };

            // Act
            await middleware.InvokeAsync(context, next);

            // Assert
            Assert.True(called);
            Assert.Equal(StatusCodes.Status200OK, context.Response.StatusCode);
        }
    }
}
