using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text;

namespace BBS.Middlewares
{
    public class SwaggerAuthenticationMiddleware : IMiddleware
    {
        private const string UserName = "admin";
        private const string Password = "admin@321";

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                string authHeader = context.Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Basic "))
                {
                    var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
                    var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword!));

                    var username = decodedUsernamePassword.Split(':', 2)[0];
                    var password = decodedUsernamePassword.Split(':', 2)[1];

                    if (IsAuthorized(username, password))
                    {
                        await next.Invoke(context);
                        return;
                    }
                }

                context.Response.Headers["WWW-Authenticate"] = "Basic";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else
            {
                await next.Invoke(context);
            }
        }

        private bool IsAuthorized(string username, string password) => UserName == username && Password == password;

        private bool IsLocalRequest(HttpContext context)
        {
            if (context.Request.Host.Value.StartsWith("localhost:"))
                return true;

            if (context.Connection.RemoteIpAddress == null && context.Connection.LocalIpAddress == null)
                return true;

            if (context.Connection.RemoteIpAddress != null && context.Connection.RemoteIpAddress.Equals(context.Connection.LocalIpAddress))
                return true;

            return IPAddress.IsLoopback(context.Connection.RemoteIpAddress!);
        }
    }
}