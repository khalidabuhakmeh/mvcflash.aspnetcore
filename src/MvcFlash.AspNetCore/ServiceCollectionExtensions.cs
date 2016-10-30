using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MvcFlash.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFlash(this IServiceCollection services)
        {
            if (services.All(x => x.ServiceType != typeof(IHttpContextAccessor)))
            {
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            }

            services.AddScoped(svc =>
            {
                var ctx = svc.GetService<IHttpContextAccessor>();
                return new Flash(ctx.HttpContext.Session);
            });

            return services;
        }
    }
}
