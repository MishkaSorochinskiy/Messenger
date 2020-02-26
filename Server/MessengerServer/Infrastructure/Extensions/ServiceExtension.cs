using Application;
using Application.IServices;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class ServiceExtension
    {
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
