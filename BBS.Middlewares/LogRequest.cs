using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NLog;

namespace BBS.Middlewares
{
    public class LogRequest
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LogRequest(RequestDelegate next, ILogger logger)
        {
            _logger = logger;
            _next = next;   
        }

        public Task InvokeAsync(HttpContext context)
        {
            throw new NotImplementedException();
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            throw new NotImplementedException();
        }
    }
    public static class LogRequestExtensions
    {
        public static IApplicationBuilder UseLogRequest(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogRequest>();
        }
    }
}
