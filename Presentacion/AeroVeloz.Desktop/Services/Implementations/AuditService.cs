using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AeroVeloz.Desktop.Models.DTOs.Audit;
using AeroVeloz.Desktop.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AeroVeloz.Desktop.Services.Implementations;

/// <summary>
/// Servicio de auditoría para la capa de presentación.
/// Consume el endpoint de auditoría del API y mapea AuditDetailModel a AuditDto.
/// </summary>
public class AuditService : IAuditService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ISessionService _sessionService;
    private readonly string _baseEndpoint;

    public AuditService(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        ISessionService sessionService)
    {
        _httpClientFactory = httpClientFactory;
        _sessionService = sessionService;
        _baseEndpoint = configuration["ApiEndpoints:Audit"] ?? "api/Audit";
    }

    /// <summary>
    /// Obtiene el historial de auditoría de un usuario específico.
    /// Llama a GET /api/Audit/GetUse/{targetUserId}?userId={userId}&orgId={orgId}
    /// </summary>
    public async Task<IEnumerable<AuditDto>> GetUserAuditAsync(Guid targetUserId)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("AeroVelozApi");
            
            // GET /api/Audit/GetUse/{targetUserId}?userId={userId}&orgId={orgId}
            var endpoint = $"{_baseEndpoint}/GetUse/{targetUserId}?userId={_sessionService.UserId}&orgId={_sessionService.OrgId}";
            var response = await client.GetAsync(endpoint);
            
            if (!response.IsSuccessStatusCode)
            {
                return new List<AuditDto>();
            }

            var content = await response.Content.ReadAsStringAsync();
            using var jsonDoc = JsonDocument.Parse(content);
            var root = jsonDoc.RootElement;

            // La respuesta es OperationResult<IReadOnlyCollection<AuditDetailModel>>
            // Estructura: { "value": [...], "success": true, ... }
            if (root.TryGetProperty("value", out var valueElement) && valueElement.ValueKind == JsonValueKind.Array)
            {
                var audits = new List<AuditDto>();
                
                foreach (var auditElement in valueElement.EnumerateArray())
                {
                    try
                    {
                        // Mapear AuditDetailModel → AuditDto
                        var auditId = auditElement.TryGetProperty("auditId", out var auditIdProp)
                            ? auditIdProp.GetString() ?? string.Empty
                            : string.Empty;
                        var auditTypeName = auditElement.TryGetProperty("auditTypeName", out var typeProp)
                            ? typeProp.GetString() ?? string.Empty
                            : string.Empty;
                        var idUser = auditElement.TryGetProperty("idUser", out var userProp)
                            ? userProp.GetString() ?? string.Empty
                            : string.Empty;
                        var nameUser = auditElement.TryGetProperty("nameUser", out var nameProp)
                            ? nameProp.GetString() ?? string.Empty
                            : string.Empty;
                        var idOrganization = auditElement.TryGetProperty("idOrganization", out var orgProp)
                            ? orgProp.GetInt32()
                            : 0;
                        var nameOrganization = auditElement.TryGetProperty("nameOrganization", out var orgNameProp)
                            ? orgNameProp.GetString() ?? string.Empty
                            : string.Empty;
                        var nameEntity = auditElement.TryGetProperty("nameEntity", out var entityProp)
                            ? entityProp.GetString() ?? string.Empty
                            : string.Empty;
                        var occurredAt = auditElement.TryGetProperty("occurredAt", out var dateProp)
                            ? dateProp.GetDateTime()
                            : DateTime.MinValue;
                        var dataNe = auditElement.TryGetProperty("dataNe", out var dataProp)
                            ? dataProp.GetString() ?? string.Empty
                            : string.Empty;

                        if (Guid.TryParse(auditId, out var parsedAuditId) && 
                            Guid.TryParse(idUser, out var parsedIdUser))
                        {
                            audits.Add(new AuditDto
                            {
                                AuditId = parsedAuditId,
                                AuditTypeName = auditTypeName,
                                IdUser = parsedIdUser,
                                NameUser = nameUser,
                                IdOrganization = idOrganization,
                                NameOrganization = nameOrganization,
                                NameEntity = nameEntity,
                                OccurredAt = occurredAt,
                                DataNew = dataNe
                            });
                        }
                    }
                    catch
                    {
                        // Ignorar auditorías problemáticas
                        continue;
                    }
                }
                
                return audits;
            }

            return new List<AuditDto>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error en GetUserAuditAsync: {ex.Message}");
            return new List<AuditDto>();
        }
    }
}
