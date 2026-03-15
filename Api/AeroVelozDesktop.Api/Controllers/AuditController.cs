using Microsoft.AspNetCore.Mvc;
using AeroVeloz.Application.Contracts.Audit;

namespace AeroVelozDesktop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditController : ControllerBase
    {

        public readonly IAuditServicie _auditService;

        public AuditController(IAuditServicie auditService) { 
            _auditService = auditService;
        }


        // GET: api/<AuditController>
        [HttpGet("GetOrgAudit/{orgId}")]
        public async Task<IActionResult> GetOrgAudit(int orgId, DateTime? from, DateTime? to, Guid userId)
        {
            var result = await _auditService.GetByOrganizationAsync(orgId, from, to, userId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        // GET api/<AuditController>/5
        [HttpGet("GetUse/{targetUserId}")]
        public async Task<IActionResult>  GetUserAudit(Guid targetUserId, DateTime? from, DateTime? to, Guid userId, int orgId)
        {
           var result = await _auditService.GetByUserAsync(targetUserId, from, to, userId, orgId);
           if (result.Success) return Ok(result);
           return BadRequest(result);
        }

       
     
    }
}
