using System.Net.Http;
using System.Net.Http.Headers;
using AeroVeloz.Desktop.Services.Interfaces;

namespace AeroVeloz.Desktop.Services.Http;


public class AuthenticationHandler : DelegatingHandler
{
    private readonly ISessionService _sessionService;

    public AuthenticationHandler(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        // Obtener el token de la sesión actual
        var token = _sessionService.Token;

        // Si existe un token válido, añadirlo al header Authorization
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        // Continuar con el siguiente handler en la cadena
        return await base.SendAsync(request, cancellationToken);
    }
}
