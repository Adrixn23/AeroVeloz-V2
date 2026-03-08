namespace AeroVeloz.Application.DTOs.Organization.Base
{
    //DTO BASE PARA LA CREACION DE LAS DIVERSAS ORGANIZACIONES
    public abstract record OrganizationBaseDto(
        string? nameOrganization,
         string? typeOrganization,
        string? emailOrganization);                                  
}
