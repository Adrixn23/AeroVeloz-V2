using AeroVeloz.Domain.Common.Enums;

namespace AeroVeloz.Application.DTOs.Organization
{
    public sealed record OrganizationSaveDto
    {
        public string? nameOrganization { get; set; }
        public OrganizationType organizationType { get; set; }
        public string? emailOrganizations { get; set; }
    }
}
