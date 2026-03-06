namespace AeroVeloz.Application.Models.Airlines
{
    public sealed record AirlineReadModel(
        string CodeAirlines,    
        string CodeIata,        
        int IdOrganization      
    );
}