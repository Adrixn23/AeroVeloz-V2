namespace AeroVeloz.Domain.Services.Interfaces.Airport
{
    public interface IDomainServiceAirport
    {
        Task<bool> ExistAirportByOrganizations(string? codeIata, string? codeIacao);
        Task<bool> AirportHasAirlineConnectionAsync(string airportCode, string airlineCode);
    }
}
