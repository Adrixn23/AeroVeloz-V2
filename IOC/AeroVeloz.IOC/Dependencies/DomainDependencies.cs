using AeroVeloz.Domain.Validators.interfaces.Airlines;
using AeroVeloz.Domain.Validators.interfaces.Flight;
using AeroVeloz.Domain.Validators.interfaces.Subscriptions;
using AeroVeloz.Domain.Validators.Orquestador.Airlines;
using AeroVeloz.Domain.Validators.Orquestador.Flights;
using AeroVeloz.Domain.Validators.Orquestador.Subscriptions;
using Microsoft.Extensions.DependencyInjection;

namespace AeroVeloz.IOC.Dependencies
{
    public static class DomainDependencies
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IFlightValidator, FlightValidatorImpl>();
            services.AddScoped<ISubscriptionValidator, SubscriptionValidatorImpl>();
            services.AddScoped<IAirlineValidator, AirlineValidatorImpl>();

            return services;
        }
    }
}
