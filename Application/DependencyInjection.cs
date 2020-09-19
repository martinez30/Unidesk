using Application.Applications;
using Application.Interfaces;
using Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IOrderApplication, OrderApplication>();
            services.AddScoped<ILightPoleApplication, LightPoleApplication>();
            services.AddScoped<ILocalizationApplication, LocalizationApplication>();
            return services;
        }
    }
}
