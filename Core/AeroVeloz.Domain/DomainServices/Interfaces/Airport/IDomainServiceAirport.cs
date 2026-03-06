namespace AeroVeloz.Domain.Services.Interfaces.Airport
{
    public interface IDomainServiceAirport
    {
        //estos metodos permiten validar si el aeropuerto que se esta intando crear ya existe dentro de la
        //organizacion  y tambien permite obtener las connnections que tiene el aeropuerto con x aerolinas
        //validando si ya la connecition existe o no existe. 

        Task<bool> ExistAirportByOrganizations(string? codeIata, string? codeIacao);
        Task<bool> AirportHasAirlineConnectionAsync(string airportCode, string airlineCode);

    }
}
