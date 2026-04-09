using System.Threading.Tasks;
using AeroVeloz.Desktop.Models.DTOs.StatusSystem;

namespace AeroVeloz.Desktop.Services.Interfaces.AdminSystem;

public interface ISuperAdminStatService
{
    Task<GlobalStatsDto?> GetGlobalStatsAsync();
}
