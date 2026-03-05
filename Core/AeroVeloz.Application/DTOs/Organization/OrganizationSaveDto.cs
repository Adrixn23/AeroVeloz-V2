using AeroVeloz.Domain.Common.Enums;

namespace AeroVeloz.Application.DTOs.Organization
{
    public sealed record OrganizationSaveDto // DTO que permite la creacion de una nueva
                                             // organization dentro del sistema, estos datos provienen de la capa de presentation 
                                             //correspondientes 
    {
        public string? nameOrganization { get; set; }
        public OrganizationType organizationType { get; set; }
        public string? emailOrganizations { get; set; }
    }
}
