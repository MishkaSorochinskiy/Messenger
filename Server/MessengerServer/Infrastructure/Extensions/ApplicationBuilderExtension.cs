using Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;

namespace Infrastructure
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            return app;
        }

        public static IApplicationBuilder UseIdHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(NameIdentifierMiddleware));

            return app;
        }
    }
}
