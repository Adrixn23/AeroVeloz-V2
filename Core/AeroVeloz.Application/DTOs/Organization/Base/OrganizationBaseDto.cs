using AeroVeloz.Domain.Common.Enums;
namespace AeroVeloz.Application.DTOs.Organization.Base
{
    //DTO BASE PARA LA CREACION DE LAS DIVERSAS ORGANIZACIONES
    public abstract record OrganizationBaseDto(
        string? nameOrganization,
        TypeOrganization Type,
        string? emailOrganization);                                  
}
