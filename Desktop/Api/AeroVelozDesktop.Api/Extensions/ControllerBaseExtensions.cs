using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace AeroVelozDesktop.Api.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static Guid GetUserId(this ControllerBase controller)
        {
            var userIdStr = controller.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userIdStr, out var userId))
                return userId;

            throw new UnauthorizedAccessException("Claim del UserId no encontrado o tiene un formato no válido.");
        }

        public static int GetOrganizationId(this ControllerBase controller)
        {
            var orgIdStr = controller.User.FindFirstValue("OrgId");
            if (int.TryParse(orgIdStr, out var orgId))
                return orgId;

            throw new UnauthorizedAccessException("Claim del OrganizationId no encontrado o tiene un formato no válido.");
        }
    }
}
