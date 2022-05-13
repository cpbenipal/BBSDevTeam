using Microsoft.AspNetCore.Http;

namespace BBS.Middlewares
{
    public class RequestLoggerMiddleware : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}
