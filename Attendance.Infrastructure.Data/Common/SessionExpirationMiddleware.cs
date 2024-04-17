using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Infrastructure.Data.Common
{
    public class SessionExpirationMiddleware : AuthorizeAttribute
    {
        private readonly RequestDelegate _next;
        public SessionExpirationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Check if the user is authenticated and the session has expired
            if (context.User.Identity.IsAuthenticated && !context.Session.Keys.Contains("LastActivity"))
            {
                // Redirect to the login page when the session has expired
                context.Response.Redirect("/Attendance/Views/Account/Login.cshtml"); 
                return;
            }

            // Reset the session expiration time on each request
            context.Session.SetString("LastActivity", DateTime.Now.ToString());

            
            await _next(context);
        }
      

    }
    public static class SessionExpirationMiddlewareExtensions
    {
        public static IApplicationBuilder UseSessionExpirationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SessionExpirationMiddleware>();
        }
    }
}

