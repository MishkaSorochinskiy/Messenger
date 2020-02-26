using Application;
using Application.IServices;
using Infrastructure.Cache;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class StartUpExtensions
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

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IPhotoService, PhotoService>();

            services.AddScoped<IMessageService, MessageService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IChatService, ChatService>();

            services.AddScoped<ICache, MemoryCache>();
        }
    }
}
