using AeroVeloz.Application.Contracts.Airlines;
using AeroVeloz.Application.Contracts.Auth;
using AeroVeloz.Application.Contracts.Flights;
using AeroVeloz.Application.Contracts.Subscriptions;
using AeroVeloz.Application.Services.Airlines;
using AeroVeloz.Application.Services.Auth;
using AeroVeloz.Application.Services.Flights;
using AeroVeloz.Application.Services.Subscriptions;
using Microsoft.Extensions.DependencyInjection;

namespace AeroVeloz.IOC.Dependencies
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationServicie, AuthenticationHandler>();
            services.AddScoped<IFlightServicie, FlightService>();
            services.AddScoped<IAirlineService, AirlineService>();
            services.AddScoped<ISubscriptionServicie, SubscriptionService>();

            //services.AddMediatR(cfg =>
            //    cfg.RegisterServicesFromAssembly(typeof(FlightService).Assembly));

            return services;
        }
    }
}
