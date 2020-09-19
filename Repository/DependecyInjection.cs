using Application;
using Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infra.Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddDbContext<Context>(x =>
            {
                x.UseSqlServer(Configuration.ConnectionString, options =>
                {
                    options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
            }, ServiceLifetime.Scoped
            );
            services.AddScoped<IContext, Context>();
            return services;
        }
    }
}
