using AeroVeloz.Application.Repositories.Operational;
using AeroVeloz.Application.DTOs.Operations;
using AeroVeloz.Domain.Entities.Operations;
using AeroVeloz.Domain.Models.Operational;
using AeroVeloz.Domain.Services.Interfaces.Operational;
using AeroVeloz.Domain.Common.Exceptions;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.Operational
{
    public class OperationalRepository : IOperationalRepository, IDomainServiceOperationalChange
    {

        private readonly AeroVelozContext _context;
        private readonly ILogger<OperationalRepository> _logger;

        public OperationalRepository(AeroVelozContext context, ILogger<OperationalRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreateEntity(OperationChange entity)
        {
            try
            {
                _context.OperationChanges.Add(entity);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la entidad de cambio operacional");
                throw new DatabaseOperationException("Error persistiendo entidad OperationChange", ex);
            }
        }

        public async Task<bool> DeleteEntity(OperationChange entity)
        {
            try
            {
                var op = await _context.OperationChanges.Where(p => p.Id == entity.Id)
                   .ExecuteUpdateAsync(setters => setters
                     .SetProperty(op => op.isActive, entity.isActive)
                   );
                return op > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al desactivar (eliminar) el cambio operacional {Id}", entity.Id);
                throw new DatabaseOperationException($"Error eliminando entidad OperationChange con Id: {entity.Id}", ex);
            }
        }

        public async Task<IReadOnlyCollection<OperationalDetailModel>> GetAirportChangesAsync(int orgId)
        {
            try
            {
                var operationsByAirport = await (
                    from op in _context.OperationChanges.AsNoTracking()
                    select new OperationalDetailModel(
                        op.Id,
                        op.idOperationalType,
                        op.flightNumber,
                        op.codeAirlinesIcao,
                        op.codeAirportIcao,
                        op.previosValue,
                        op.newValue,
                        op.changeAt,
                        op.cause,
                        op.isActive
                    )).ToListAsync();

                if(operationsByAirport.Any()) 
                    return operationsByAirport;

                return Array.Empty<OperationalDetailModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo cambios operacionales por aeropuerto (orgId: {OrgId})", orgId);
                return Array.Empty<OperationalDetailModel>();
            }
        }

        public async Task<OperationalModel> GetByOperationAsync(Guid operationId)
        {
            try
            {
                var operation =  await (
                          from op in _context.OperationChanges.AsNoTracking()
                          join t in _context.OperationalChangeTypes.AsNoTracking()
                          on op.idOperationalType equals t.Id
                          where op.Id == operationId
                          select new OperationalModel(op.idUser, op.Id, t.name, op.changeAt, op.cause)

                     ).FirstOrDefaultAsync();

                if(operation == null )
                     return null!;
                return operation;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo la operación por Id {OperationId}", operationId);
                return null!;
            }
        }

        public async Task<IReadOnlyCollection<OperationalModel>> GetFlightChangesAsync(short flightNumber)
        {
            try
            {
                var operationsByFlight = await (
                         from op in _context.OperationChanges.AsNoTracking()
                         join t in _context.OperationalChangeTypes.AsNoTracking()
                         on  op.idOperationalType equals t.Id
                         where op.flightNumber == flightNumber
                         select new OperationalModel(op.idUser, op.Id, t.name, op.changeAt, op.cause)

                    ).ToListAsync();
                if (operationsByFlight.Any())
                    return operationsByFlight;
                return Array.Empty<OperationalModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo cambios por número de vuelo {FlightNumber}", flightNumber);
                return Array.Empty<OperationalModel>();
            }
        }

        public async Task<IReadOnlyCollection<FlightOperationDto>> GetFlightOperationsAsync(short flightNumber)
        {
            try
            {
                var operations = await (
                    from op in _context.OperationChanges.AsNoTracking()
                        .Where(op => op.flightNumber == flightNumber)
                    join t in _context.OperationalChangeTypes.AsNoTracking() 
                        on op.idOperationalType equals t.Id
                    select new FlightOperationDto
                    {
                        Id = op.Id,
                        IdOperationalType = op.idOperationalType,
                        OperationalTypeName = t.name,
                        FlightNumber = op.flightNumber,
                        CodeAirline = op.codeAirlinesIcao,
                        CodeAirport = op.codeAirportIcao,
                        PreviousValue = op.previosValue,
                        NewValue = op.newValue,
                        ChangeAt = op.changeAt,
                        Cause = op.cause,
                        IsActive = op.isActive,
                        UserId = op.idUser
                    }
                ).OrderByDescending(op => op.ChangeAt)
                 .ToListAsync();

                return operations;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo operaciones para el vuelo {FlightNumber}", flightNumber);
                return Array.Empty<FlightOperationDto>();
            }
        }

        public async Task<bool> OperationAlreadyRegisteredAsync(Guid operationId, short typeOp)
        {
            try
            {
                var operation = await _context.OperationChanges.FirstOrDefaultAsync(op => op.Id == operationId);
                if(operation == null) return false;
                if (operation.idOperationalType == typeOp) return false;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error comprobando OperationAlreadyRegisteredAsync (opId: {OperationId}, typeOp: {TypeOp})", operationId, typeOp);
                return false;
            }
        }

        public async Task<bool> OperationBelongsToOrganizationAsync(Guid operationId, int organizationId)
        {
            try
            {
                var organization = await _context.Organizations.FirstOrDefaultAsync(org => org.Id == organizationId);
                if(organization == null)
                {
                    return false;
                }
               if(!organization.isActived) return false;
               var operation = await _context.OperationChanges.FirstOrDefaultAsync(op => op.Id == operationId);
               if (operation == null) return false; 
               return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error comprobando pertenececia de org a la operacion {OpId}", operationId);
                return false;
            }
        }

        public async Task<bool> OperationConsultFlightValid(short flightNumber)
        {
            try
            {
                var fl = await _context.Flights.FirstOrDefaultAsync(f => f.Id == flightNumber);
                if (fl == null) return false;
                if (DateTimeOffset.UtcNow - fl.ScheduledDeparture > TimeSpan.FromDays(2)) return false;
                var stateF = await _context.FlightStates.FirstOrDefaultAsync(fs => fs.Id == fl.flightStatesId);
                if(stateF == null) return false;
                if(stateF.name == "CANCELLED" || stateF.name == "InFlight") return false;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validando consulta de vuelo {Flight}", flightNumber);
                return false;
            }
        }

        public async Task<bool> OperationExistsAsync(Guid operationId)
        {
            try
            {
                var operation = await _context.OperationChanges.FirstOrDefaultAsync(op => op.Id  == operationId);
                return operation != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validando existencia de operacion {OpId}", operationId);
                return false;
            }
        }

        public async Task<bool> UpdateEntity(OperationChange entity)
        {
            try
            {
                var operationUpdate = await _context.OperationChanges.Where(operation => operation.Id == entity.Id)
                    .ExecuteUpdateAsync(setters => setters
                    .SetProperty(op => op.idOperationalType, entity.idOperationalType)
                    .SetProperty(op => op.flightNumber, entity.flightNumber)
                    .SetProperty(op => op.codeAirlinesIcao, entity.codeAirlinesIcao)
                    .SetProperty(op => op.codeAirportIcao, entity.codeAirportIcao)
                    .SetProperty(op => op.isActive, entity.isActive)
                    .SetProperty(op => op.previosValue, entity.previosValue)
                    .SetProperty(op => op.newValue, entity.newValue)
                    .SetProperty(op => op.cause, entity.cause)
                    );

                return operationUpdate > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error actualizando la entidad OperationChange {Id}", entity.Id);
                throw new DatabaseOperationException($"Error actualizando entidad OperationChange con Id: {entity.Id}", ex);
            }
        }

        public async Task<string?> GetOperationalTypeNameAsync(short typeId)
        {
            try
            {
                var type = await _context.OperationalChangeTypes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Id == typeId);
                return type?.name;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo typo op {id}", typeId);
                return null;
            }
        }
    }
}
