namespace AeroVeloz.Domain.DomainServices.Interfaces.Airport
{
    public interface IAiportExternarDomainServiceValidator
    {
        Task<bool> ValidateAirport(string iata, string icao);
    }
}
