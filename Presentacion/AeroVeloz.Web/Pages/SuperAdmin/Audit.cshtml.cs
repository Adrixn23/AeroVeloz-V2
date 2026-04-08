using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using AeroVeloz.Web.Models.Audit;
using AeroVeloz.Web.Services.Interfaces;

namespace AeroVeloz.Web.Pages.SuperAdmin
{
    [Authorize(Roles = "SYSTEMADMIN")]
    public class AuditModel : PageModel
    {
        private readonly IAuditApiService _auditService;

        public AuditModel(IAuditApiService auditService)
        {
            _auditService = auditService;
        }

        public List<AuditDetailDto> Audits { get; set; } = new();
        public string? ErrorMessage { get; set; }
        public Guid? FilteredUserId { get; set; }

        public async Task OnGetAsync(Guid? userIdFilter = null)
        {
            FilteredUserId = userIdFilter;
            var token = User.Claims.FirstOrDefault(c => c.Type == "JwtToken")?.Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type =="userId")?.Value ?? User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var orgIdClaim = User.Claims.FirstOrDefault(c => c.Type == "OrganizationId")?.Value;

            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(userId) && int.TryParse(orgIdClaim, out int orgId))
            {
                try
                {
                    if (userIdFilter.HasValue)
                    {
                        Audits = await _auditService.GetUserAuditsAsync(userIdFilter.Value, token, userId, orgId);
                    }
                    else
                    {
                        Audits = await _auditService.GetOrganizationAuditsAsync(orgId, token, userId);
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Error al cargar auditoría: {ex.Message}";
                }
            }
        }
    }
}
