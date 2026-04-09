using System.Net.Http;
using System.Text.Json;
using AeroVeloz.Desktop.Models.DTOs.Audit;
using AeroVeloz.Desktop.Services.Interfaces.Audit;
using AeroVeloz.Desktop.Services.Interfaces.Auth;
using Microsoft.Extensions.Configuration;

namespace AeroVeloz.Desktop.Services.Implementations.Audit;


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


    public async Task<IEnumerable<AuditDto>> GetUserAuditAsync(Guid targetUserId)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("AeroVelozApi");
            
            var endpoint = $"{_baseEndpoint}/GetUse/{targetUserId}?userId={_sessionService.UserId}&orgId={_sessionService.OrgId}";
            var response = await client.GetAsync(endpoint);
            
            if (!response.IsSuccessStatusCode)
            {
                return new List<AuditDto>();
            }

            var content = await response.Content.ReadAsStringAsync();
            using var jsonDoc = JsonDocument.Parse(content);
            var root = jsonDoc.RootElement;

            if (root.TryGetProperty("value", out var valueElement) && valueElement.ValueKind == JsonValueKind.Array)
            {
                var audits = new List<AuditDto>();
                
                foreach (var auditElement in valueElement.EnumerateArray())
                {
                    try
                    {
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

   
    public async Task<IEnumerable<AuditDto>> GetGlobalAuditAsync(int orgId)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("AeroVelozApi");

            var endpoint = $"{_baseEndpoint}/GetOrgAudit/{orgId}?userId={_sessionService.UserId}";
            var response = await client.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
            {
                return new List<AuditDto>();
            }

            var content = await response.Content.ReadAsStringAsync();
            using var jsonDoc = JsonDocument.Parse(content);
            var root = jsonDoc.RootElement;

            if (root.TryGetProperty("value", out var valueElement) && valueElement.ValueKind == JsonValueKind.Array)
            {
                var audits = new List<AuditDto>();

                foreach (var auditElement in valueElement.EnumerateArray())
                {
                    try
                    {
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
                        continue;
                    }
                }

                return audits;
            }

            return new List<AuditDto>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error en GetGlobalAuditAsync: {ex.Message}");
            return new List<AuditDto>();
        }
    }
}
