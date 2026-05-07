namespace AeroVeloz.Desktop.Models.DTOs.StatusSystem;

public class AirportAdminStatsDto
{
    public int ContactedAirlines { get; set; }
    public int TotalOperators { get; set; }
    public int ActiveConnections { get; set; }
    public int PendingOperations { get; set; }
}
