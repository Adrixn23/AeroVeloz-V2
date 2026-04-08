using System.Collections.Generic;
using System.Threading.Tasks;
using AeroVeloz.Desktop.Models.DTOs.Airport;

namespace AeroVeloz.Desktop.Services.Interfaces;

public interface IAirportService
{
    Task<IEnumerable<AirportDto>> GetAllAsync();
    Task<AirportDto?> GetByIdAsync(int id);
    Task<AirportDto?> CreateAsync(CreateAirportDto createAirportDto);
    Task<bool> UpdateAsync(int id, AirportDto airportDto);
    Task<bool> DeleteAsync(int id);
}
