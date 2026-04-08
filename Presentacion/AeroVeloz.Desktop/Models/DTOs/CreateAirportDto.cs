using System;

namespace AeroVeloz.Desktop.Models.DTOs;

public class CreateAirportDto
{
    public string NameOrganization { get; set; } = string.Empty;
    public string EmailOrganization { get; set; } = string.Empty;
    public string CodeAirportIcao { get; set; } = string.Empty;
    public string CodeAirportIata { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public DateTimeOffset TimeOffset { get; set; }
}
