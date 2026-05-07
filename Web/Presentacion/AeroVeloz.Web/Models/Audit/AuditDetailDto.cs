using System.Text.Json.Serialization;

namespace AeroVeloz.Web.Models.Audit
{
    public class AuditDetailDto
    {
        [JsonPropertyName("auditId")]
        public Guid AuditId { get; set; }

        [JsonPropertyName("auditTypeName")]
        public string? AuditTypeName { get; set; }

        [JsonPropertyName("idUser")]
        public Guid IdUser { get; set; }

        [JsonPropertyName("nameUser")]
        public string? NameUser { get; set; }

        [JsonPropertyName("idOrganization")]
        public int IdOrganization { get; set; }

        [JsonPropertyName("nameOrganization")]
        public string? NameOrganization { get; set; }

        [JsonPropertyName("nameEntity")]
        public string? NameEntity { get; set; }

        [JsonPropertyName("occurredAt")]
        public DateTime OccurredAt { get; set; }

        [JsonPropertyName("dataNew")]
        public string? DataNew { get; set; }
    }
}
