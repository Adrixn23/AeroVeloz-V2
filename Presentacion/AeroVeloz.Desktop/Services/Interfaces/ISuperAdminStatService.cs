using System.Threading.Tasks;
using AeroVeloz.Desktop.Models.DTOs.StatusSystem;

namespace AeroVeloz.Desktop.Services.Interfaces;

public interface ISuperAdminStatService
{
    Task<GlobalStatsDto?> GetGlobalStatsAsync();
}
