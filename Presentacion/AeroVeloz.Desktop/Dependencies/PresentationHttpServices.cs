using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AeroVeloz.Desktop.Services.Http;

namespace AeroVeloz.Desktop.Dependencies;


public static class PresentationHttpServices
{
    public static IServiceCollection AddPresentationHttpServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        var baseUrl = configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7126";

        services.AddTransient<AuthenticationHandler>();
        services.AddTransient<HttpErrorInterceptorHandler>();

      
        services.AddHttpClient("AeroVelozApi", client =>
        {
            client.BaseAddress = new Uri(baseUrl);
        })
        .AddHttpMessageHandler<AuthenticationHandler>()
        .AddHttpMessageHandler<HttpErrorInterceptorHandler>();

        return services;
    }
}
