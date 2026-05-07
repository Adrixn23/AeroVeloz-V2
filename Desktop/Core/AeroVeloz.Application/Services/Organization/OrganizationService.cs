using AeroVeloz.Application.Contracts.Organization;
using AeroVeloz.Application.DTOs.Organization;
using AeroVeloz.Application.Handlers.Result;
using AeroVeloz.Domain.DomainServices.Interfaces.Organization;
using AeroVeloz.Transversal.Contracts.Monitoring;

namespace AeroVeloz.Application.Services.Organization
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IDomainServiceOrganization _domainService;
        private readonly IOrganizationMonitoringLogger _monitoringLogger;

        public OrganizationService(
            IDomainServiceOrganization domainService,
            IOrganizationMonitoringLogger monitoringLogger)
        {
            _domainService = domainService;
            _monitoringLogger = monitoringLogger;
        }

        public async Task<OperationResult<IEnumerable<OrganizationDto>>> GetOrganizationsByTypeAsync(string type)
        {
            try
            {
                var organizations = await _domainService.GetByTypeAsync(type);
                
                var dtos = organizations.Select(o => new OrganizationDto(
                    o.Id,
                    o.NameOrganization,
                    o.TypeOrganization,
                    o.IsActive,
                    o.EmailOrganization
                )).ToList();

                return OperationResult<IEnumerable<OrganizationDto>>.Ok(dtos);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<OrganizationDto>>.Fail("ORG_QUERY_ERROR", 
                    $"Error al obtener organizaciones: {ex.Message}");
            }
        }

        public async Task<OperationResult<bool>> BlockOrganizationAsync(int orgId)
        {
            try
            {
                var org = await _domainService.GetByIdAsync(orgId);
                if (org == null)
                    return OperationResult<bool>.Fail("ORG_NOT_FOUND", "Organización no encontrada");

                var success = await _domainService.UpdateOrganizationStatusAsync(orgId, false);
                
                if (success)
                {
                    return OperationResult<bool>.Ok(true, "Organización bloqueada exitosamente");
                }
                else
                {
                    return OperationResult<bool>.Fail("ORG_UPDATE_ERROR", "No se pudo bloquear la organización");
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Fail("ORG_BLOCK_ERROR", 
                    $"Error al bloquear organización: {ex.Message}");
            }
        }
    }
}
