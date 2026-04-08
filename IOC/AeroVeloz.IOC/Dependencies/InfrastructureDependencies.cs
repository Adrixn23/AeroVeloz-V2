using AeroVeloz.Application.Repositories.Airport;
using AeroVeloz.Application.Repositories.Audit;
using AeroVeloz.Application.Repositories.Auth;
using AeroVeloz.Application.Repositories.Operational;
using AeroVeloz.Application.Repositories.StatusSystem;
using AeroVeloz.Application.Repositories.Users;
using AeroVeloz.Domain.DomainServices.Interfaces.Organization;
using AeroVeloz.Domain.DomainServices.Interfaces.User;
using AeroVeloz.Domain.Services.Interfaces.Airport;
using AeroVeloz.Domain.Services.Interfaces.Operational;
using AeroVeloz.Infraestructure.Persistence.Monitoring;
using AeroVeloz.Infraestructure.Persistence.Repositories.Airport;
using AeroVeloz.Infraestructure.Persistence.Repositories.Audit;
using AeroVeloz.Infraestructure.Persistence.Repositories.Auth;
using AeroVeloz.Infraestructure.Persistence.Repositories.Operational;
using AeroVeloz.Infraestructure.Persistence.Repositories.Organization;
using AeroVeloz.Infraestructure.Persistence.Repositories.StatusSystem;
using AeroVeloz.Infraestructure.Persistence.Repositories.User;
using AeroVeloz.Transversal.Contracts.Monitoring;
using AeroVeloz.Application.Contracts.Auth;
using AeroVeloz.Infraestructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace AeroVeloz.IOC.Dependencies
{
    public static class InfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IAirportRepository, AirportRepository>();
            services.AddScoped<IAirportConnectionAirline, AirportConnectionAirline>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOperationalRepository, OperationalRepository>();
            services.AddScoped<IAuditRepository, AuditRepository>();
            services.AddScoped<IUserRepositoryAuthenticacion, UserAuthenticationRepository>();
            services.AddScoped<IUserRepositoryAuthorization, UserRepositoryAuthorization>();
            services.AddScoped<IStatsRepository, StatsRepository>();

            // Domain services implementados en Infrastructure
            services.AddScoped<IDomainServiceAirport, AirportRepository>();
            services.AddScoped<IDomainServiceUser, UserRepository>();
            services.AddScoped<IDomainServiceOperationalChange, OperationalRepository>();
            services.AddScoped<IDomainServiceOrganization, OrganizationRepository>();

            // Monitoring transversal
            services.AddSingleton<IOrganizationMonitoringLogger, OrganizationMonitoringLogger>();
            services.AddSingleton<IMonitoringLogReader, OrganizationMonitoringLogReader>();

            return services;
        }
    }
}
