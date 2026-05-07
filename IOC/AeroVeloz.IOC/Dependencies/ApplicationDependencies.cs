using AeroVeloz.Application.Contracts.Airport;
using AeroVeloz.Application.Contracts.Audit;
using AeroVeloz.Application.Contracts.Auth;
using AeroVeloz.Application.Contracts.Flights;
using AeroVeloz.Application.Contracts.Operations;
using AeroVeloz.Application.Contracts.StatusSystem;
using AeroVeloz.Application.Contracts.Users;
using AeroVeloz.Application.Contracts.Organization;
using AeroVeloz.Application.Handlers.Airport;
using AeroVeloz.Application.Handlers.Audit;
using AeroVeloz.Application.Handlers.Auth;
using AeroVeloz.Application.Handlers.Operations;
using AeroVeloz.Application.Handlers.Users;
using AeroVeloz.Application.Services.Flights;
using AeroVeloz.Application.Services.StatusSystem;
using AeroVeloz.Application.Services.Organization;
using Microsoft.Extensions.DependencyInjection;

namespace AeroVeloz.IOC.Dependencies
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAirportService, AirportService>();
            services.AddScoped<IAirportConnectionService, AirportConnectionService>();
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOperationalService, OperationalService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAuditService, AuditService>();
            services.AddScoped<IStatsService, StatsService>();
            services.AddScoped<IOrganizationService, OrganizationService>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AirportService).Assembly));

            return services;
        }
    }
}
