using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using AeroVeloz.Desktop.Models.DTOs.Result.ApiResponse;
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
                    if (request.RequestUri != null && request.RequestUri.AbsolutePath.Contains("/login", StringComparison.OrdinalIgnoreCase))
                    {
                        return response;
                    }

                    try 
                    {
                        var options = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var errorResponse = await response.Content!.ReadFromJsonAsync<ApiErrorResponseDto>(options, cancellationToken: cancellationToken);

                        if (errorResponse != null)
                        {
                            string finalMessage = string.Empty;

                            if (errorResponse.ValidationErrors != null && errorResponse.ValidationErrors.Length > 0)
                            {
                                var errorDescriptions = errorResponse.ValidationErrors
                                    .Select(e => $"• {e.Description}")
                                    .Where(msg => !string.IsNullOrWhiteSpace(msg));

                                string details = string.Join("\n", errorDescriptions);
                                finalMessage = $"Por favor, revisa lo siguiente:\n\n{details}";
                            }
                            else if (!string.IsNullOrEmpty(errorResponse.Message) && !errorResponse.Message.Contains("Errores de validación", StringComparison.OrdinalIgnoreCase))
                            {
                                finalMessage = errorResponse.Message;
                            }
                            else
                            {
                                finalMessage = "Algunos datos ingresados no son válidos. Por favor, verifica e intenta de nuevo.";
                            }

                            await _dialogService.ShowErrorAsync(finalMessage, "Datos Inválidos");
                        }
                    }
                    catch 
                    {
                        await _dialogService.ShowErrorAsync("La petición era inválida. Verifica los datos ingresados.", "Datos Inválidos");
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
