namespace Usuarios.ApiService.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LogginMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogginMiddleware> _logger;

        public LogginMiddleware(RequestDelegate next, ILogger<LogginMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation("=============== REQUEST FEITA NO ENDPOINT: {Path}", httpContext.Request.Path.ToString().ToUpper());

            await _next(httpContext);

            _logger.LogInformation("=============== STATUS CODE DA REQUEST: {StatusCode}", httpContext.Response.StatusCode);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseLogginMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogginMiddleware>();
        }
    }
}