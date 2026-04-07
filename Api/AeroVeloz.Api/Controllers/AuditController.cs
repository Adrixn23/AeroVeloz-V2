using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AeroVeloz.Application.Contracts.Audit;
using AeroVeloz.Domain.Models.Audit;

namespace AeroVeloz.Api.Controllers
{
    [Route("api/audits")]
    [Authorize]
    public class AuditController : ApiBaseController
    {
        private readonly IAuditServicie _auditService;

        public AuditController(IAuditServicie auditService)
        {
            _auditService = auditService;
        }

        [HttpGet("organization/{orgId:int}")]
        public async Task<ActionResult<IReadOnlyCollection<AuditDetailModel>>> GetOrgAudit(int orgId, [FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromHeader] Guid userId)
        {
            var result = await _auditService.GetByOrganizationAsync(orgId, from, to, userId);
            return ProcessResult(result);
        }

        [HttpGet("user/{targetUserId:guid}")]
        public async Task<ActionResult<IReadOnlyCollection<AuditDetailModel>>> GetUserAudit(Guid targetUserId, [FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromHeader] Guid userId, [FromHeader] int orgId)
        {
            var result = await _auditService.GetByUserAsync(targetUserId, from, to, userId, orgId);
            return ProcessResult(result);
        }
    }
}
