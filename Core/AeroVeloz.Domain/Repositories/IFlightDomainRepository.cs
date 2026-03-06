using System.Collections.Generic;
namespace AeroVeloz.Domain.Repositories
{
    public interface IFlightDomainRepository
    {// Verifica si ya existe un vuelo con ese número y cdigo de aerolinea
        // Usado por: IFlightDomainService, para evitar duplicados al crear
        Task<bool> ExistsFlightAsync(short flightNumber, string airlineCode);

        // Verifica si el aeropuerto de origen existe y está activo
              // Usado por: IFlightDomainService>IsvalidOriginAirport
        Task<bool> IsOriginAirportActiveAsync(string airportCode);


        //verifica si la aerolinea es dueña de el vuelo. 
        // Usado por: IFlightDomainService:GetcodeAirlinesOwner

        Task<bool> IsAirlineOwnerOfFlightAsync(short flightNumber, string airlineCode);

        // Verifica si la organización a la que pertenece la aerolínea está activa
        // Usado por: IFlightDomainService: GetFlightidNumber
        Task<bool> IsOrganizationActiveAsync(int idOrganization);


    }
}
