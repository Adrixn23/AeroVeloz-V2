using System.Net.Http;
using System.Net.Http.Headers;
using AeroVeloz.Desktop.Services.Interfaces.Auth;

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
       
        var token = _sessionService.Token;
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
