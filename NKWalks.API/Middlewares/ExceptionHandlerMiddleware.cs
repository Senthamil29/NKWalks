using System.Net;

namespace NKWalks.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger,RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();

                // Log this Exception
                logger.LogError(ex, $"{errorId} : {ex.Message}");

                // Return a Custom Exception
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var errorMsg = new
                {
                    Id = errorId,
                    ErrorMessage = "Something Went Wrong! We are looking into resolving this."
                };

                await httpContext.Response.WriteAsJsonAsync(errorMsg);
            }
        }
    }
}
