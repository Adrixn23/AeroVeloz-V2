using System.Collections.Generic;
using System.Threading.Tasks;
using AeroVeloz.Desktop.Models.DTOs.Audit;

namespace AeroVeloz.Desktop.Services.Interfaces;

public interface IAuditService
{
    /// <summary>
    /// Obtiene el historial de auditoría para un usuario específico.
    /// </summary>
    Task<IEnumerable<AuditDto>> GetUserAuditAsync(Guid targetUserId);
}
