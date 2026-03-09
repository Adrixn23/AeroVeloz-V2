using AeroVeloz.Application.Repositories.Audit;
using AeroVeloz.Application.Repositories.Auth;
using AeroVeloz.Application.Repositories.Flights;
using AeroVeloz.Application.Repositories.Notifications;
using AeroVeloz.Application.Repositories.Subscriptions;
using AeroVeloz.Domain.DomainService.Interfaces.Airlines;
using AeroVeloz.Domain.DomainService.Interfaces.Flights;
using AeroVeloz.Domain.DomainService.Interfaces.Organization;
using AeroVeloz.Domain.DomainService.Interfaces.Subscriptions;
using AeroVeloz.Infraestructure.Persistence.Monitoring;
using AeroVeloz.Infraestructure.Persistence.Repositories.Airlines;
using AeroVeloz.Infraestructure.Persistence.Repositories.Audit;
using AeroVeloz.Infraestructure.Persistence.Repositories.Auth;
using AeroVeloz.Infraestructure.Persistence.Repositories.Flights;
using AeroVeloz.Infraestructure.Persistence.Repositories.Notifications;
using AeroVeloz.Infraestructure.Persistence.Repositories.Organization;
using AeroVeloz.Infraestructure.Persistence.Repositories.Subscription;
using AeroVeloz.Transversal.Contracts.Monitoring;
using Microsoft.Extensions.DependencyInjection;

namespace AeroVeloz.IOC.Dependencies
{
    public static class InfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Auth repositories
            services.AddScoped<IUserRepositoryAuthenticacion, UserAuthenticationRepository>();
            services.AddScoped<IUserRepositoryAuthorization, UserRepositoryAuthorization>();

            // Module repositories
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IAuditWriteRepository, AuditWriteRepository>();

            // Domain services implemented in Infrastructure
            services.AddScoped<IDomainServiceOrganization, OrganizationRepository>();
            services.AddScoped<IFlightDomainService, FlightDomainServiceImpl>();
            services.AddScoped<IAirlineDomainService, AirlineDomainServiceImpl>();
            services.AddScoped<ISubscriptionsDomainService, SubscriptionDomainServiceImpl>();

            // Transversal monitoring
            services.AddScoped<IOrganizationMonitoringLogger, OrganizationMonitoringLogger>();

            return services;
        }
    }
}
