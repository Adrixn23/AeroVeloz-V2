using AeroVeloz.Application.Contracts.Airport;
using AeroVeloz.Application.Contracts.Audit;
using AeroVeloz.Application.Contracts.Auth;
using AeroVeloz.Application.Contracts.Operations;
using AeroVeloz.Application.Contracts.Users;
using AeroVeloz.Application.Handlers.Airport;
using AeroVeloz.Application.Handlers.Audit;
using AeroVeloz.Application.Handlers.Auth;
using AeroVeloz.Application.Handlers.Operations;
using AeroVeloz.Application.Handlers.Users;
using Microsoft.Extensions.DependencyInjection;

namespace AeroVeloz.IOC.Dependencies
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAirportServicie, AirportServicie>();
            services.AddScoped<IAirportConnectionServicie, AirportConnectionService>();
            services.AddScoped<IUserServicie, UserService>();
            services.AddScoped<IOperationalServicie, OperationalService>();
            services.AddScoped<IAuthenticationServicie, AuthenticationService>();
            services.AddScoped<IAuditServicie, AuditService>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AirportServicie).Assembly));

            return services;
        }
    }
}
