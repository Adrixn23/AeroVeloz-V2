using AeroVeloz.Domain.DomainServices.Interfaces.User;
using AeroVeloz.Domain.Services.Interfaces.Airport;
using AeroVeloz.Domain.Services.Interfaces.Operational;
using AeroVeloz.Domain.DomainServices.Interfaces.Organization;
using AeroVeloz.Domain.Validators.interfaces.Airports;
using AeroVeloz.Domain.Validators.interfaces.Airport;
using AeroVeloz.Domain.Validators.interfaces.Operations;
using AeroVeloz.Domain.Validators.interfaces.SuperAdminValidator;
using AeroVeloz.Domain.Validators.Orquestador.Airport;
using AeroVeloz.Domain.Validators.Orquestador.Operational;
using AeroVeloz.Domain.Validators.Orquestador.SuperAdmin;
using Microsoft.Extensions.DependencyInjection;

namespace AeroVeloz.IOC.Dependencies
{
    public static class DomainDependencies
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IAirportValidator, AirportValidator>();
            services.AddScoped<IConnectionAiportAirline, ConnectionAiportAirline>();
            services.AddScoped<IUserValidator, UserValidator>();
            services.AddScoped<IOperationalChangeValidator, OperationalChangeValidator>();

            return services;
        }
    }
}
