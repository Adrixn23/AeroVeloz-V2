using AeroVeloz.Desktop.Models.DTOs.StatusSystem;

namespace AeroVeloz.Desktop.Services.Interfaces.Airport;

public interface IAirportAdminStatService
{
    Task<AirportAdminStatsDto?> GetAirportStatsAsync();
}
