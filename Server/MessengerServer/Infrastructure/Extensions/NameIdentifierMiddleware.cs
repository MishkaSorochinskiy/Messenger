using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public class NameIdentifierMiddleware
    {
        private readonly RequestDelegate _next;

        public NameIdentifierMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                context.Items["id"] =
                     Convert.ToInt32(context.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }

            await _next(context);       
        }
    }
}
