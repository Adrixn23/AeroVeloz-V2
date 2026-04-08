using System.Net.Http;
using System.Net.Http.Json;
using AeroVeloz.Desktop.Models.DTOs;
using AeroVeloz.Desktop.Services.Dialog;

namespace AeroVeloz.Desktop.Services.Http;

public class HttpErrorInterceptorHandler : DelegatingHandler
{
    private readonly IDialogService _dialogService;

    public HttpErrorInterceptorHandler(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                if (response.Content != null)
                {
                    await response.Content.LoadIntoBufferAsync();
                }

                if ((int)response.StatusCode == 400)
                {
                    try 
                    {
                        var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponseDto>(cancellationToken: cancellationToken);
                        if (errorResponse != null && !string.IsNullOrEmpty(errorResponse.Message))
                        {
                            await _dialogService.ShowErrorAsync(errorResponse.Message, "Error de Validación (400)");
                        }
                    }
                    catch 
                    {
                        await _dialogService.ShowErrorAsync("La petición era inválida.", "Bad Request");
                    }
                }
                else if ((int)response.StatusCode == 401 || (int)response.StatusCode == 403)
                {
                    await _dialogService.ShowErrorAsync("Acceso denegado o sesión expirada.", "Problema de Autenticación");
                }
                else if ((int)response.StatusCode >= 500)
                {
                    await _dialogService.ShowErrorAsync("Ocurrió un error en el servidor.", "Error Crítico");
                }
            }

            return response;
        }
        catch (HttpRequestException ex)
        {
            await _dialogService.ShowErrorAsync($"Fallo de conexión con el API: {ex.Message}", "Sin Conexión");
            throw; 
        }
        catch (TaskCanceledException)
        {
            await _dialogService.ShowErrorAsync("El tiempo de conexión expiró (Timeout).", "Timeout");
            throw;
        }
        catch (Exception ex)
        {
            await _dialogService.ShowErrorAsync($"Error HTTP inesperado: {ex.Message}", "Excepción de Red");
            throw;
        }
    }
}
