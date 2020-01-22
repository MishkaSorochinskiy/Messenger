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
            services.AddSingleton<AuthService>();

            services.AddSingleton<IPhotoService, PhotoService>();

            services.AddSingleton<IMessageService, MessageService>();

            services.AddSingleton<IUserService, UserService>();
        }
    }
}
