using AeroVeloz.Application.Repositories.Operational;
using AeroVeloz.Domain.Common.Enums.Organization;
using AeroVeloz.Domain.Entities.Operations;
using AeroVeloz.Domain.Models.Operational;
using AeroVeloz.Domain.Services.Interfaces.Operational;
using AeroVeloz.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.Operational
{
    public class OperationalRepository : IOperationalRepository, IDomainServiceOperationalChange
    {

        private readonly AeroVelozContext _context;

        public OperationalRepository(AeroVelozContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateEntity(OperationChange entity)
        {
            var operation = new Persistence.Entities.OperationChange
            {
                OperationId = entity.Id,
                IdUser = entity.idUser,
                IdOperationalType = (short)entity.operationType,
                FlighNumber = (short)entity.flightNumber!,
                CodeAirline = entity.codeAirline!,
                PrivousValue = entity.previosValue!,
                NewValue = entity.newValue!,
                Cause = entity.cause!
            };
            _context.OperationChanges.Add(operation);
            var result = await _context.SaveChangesAsync();
            return result > 0;
            
        }

        public Task<bool> DeleteEntity(OperationChange entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> FlightExistsAsync(short flightNumber)
        {
            var flight = await _context.Flights.FirstOrDefaultAsync(fl => fl.FlightNumber == flightNumber);
            if(flight == null) return false;
            return true;
        }

      
        public Task<IReadOnlyCollection<OperationalModel>> GetAirportChangesAsync(int orgId)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationalModel?> GetByIdAsync(Guid id)
        {
            var operation = await _context.OperationChanges.FirstOrDefaultAsync(op => op.OperationId == id);
            if(operation != null)
            {
                return new OperationalModel(
                    operation.IdUser,
                    operation.OperationId,
                    Enum.Parse<OperationalChangeType>(operation.IdOperationalTypeNavigation.Name),
                    operation.ChangeAt,
                    operation.Cause
                    );
            }
            return null!;
        }

        public async Task<OperationalModel> GetByOperationAsync(Guid operationId)
        {
            var operation = await _context.OperationChanges.FirstOrDefaultAsync(op => op.OperationId == operationId);
            if (operation == null)  return null!;
            return new OperationalModel(
                   operation.IdUser,
                   operation.OperationId,
                   Enum.Parse<OperationalChangeType>(operation.IdOperationalTypeNavigation.Name),
                   operation.ChangeAt,
                   operation.Cause
                );
            
        }

        public async Task<IReadOnlyCollection<OperationalModel>> GetFlightChangesAsync(short flightNumber)
        {
             var operationesByFlight = await _context.OperationChanges.Where(op => op.FlighNumber == flightNumber)
                .Select(op => new OperationalModel(
                          op.IdUser,
                          op.OperationId,
                          Enum.Parse<OperationalChangeType>(op.IdOperationalTypeNavigation.Name),
                          op.ChangeAt,
                          op.Cause
                    )).ToListAsync();
            if (operationesByFlight.Any())
                return operationesByFlight;

            return Array.Empty<OperationalModel>(); 

        }

        public async Task<bool> OperationAlreadyRegisteredAsync(Guid operationId, OperationalChangeType type)
        {
            var operation = await _context.OperationChanges.FirstOrDefaultAsync(op => op.OperationId == operationId);
            if(operation == null) return false;
            //if(operation.IdOperationalTypeNavigation.IdOperationalType == (int) type.  ) 
            // o

            return true;
        }

        public async Task<bool> OperationBelongsToOrganizationAsync(Guid operationId, int organizationId)
        {
            var organization = await _context.Organizations.FirstOrDefaultAsync(org => org.IdOrganizations == organizationId);
            if(organization == null)
            {
                return false;
            }
           if(organization.IsActive == false) return false;
           var operation = await _context.OperationChanges.FirstOrDefaultAsync(op => op.OperationId == operationId);
           if (operation == null) return false; 
           return true;
        }

        public async Task<bool> OperationExistsAsync(Guid operationId)
        {
            var operation = await _context.OperationChanges.FirstOrDefaultAsync(op => op.OperationId  == operationId);
            return operation != null;
        }

        public async Task<bool> UpdateEntity(OperationChange entity)
        {
            var operation = await _context.OperationChanges.FirstOrDefaultAsync(op => op.OperationId == entity.Id);
            if (operation == null) return false;

            operation.FlighNumber = (short)entity.flightNumber!;
            operation.CodeAirline = entity.codeAirline!;
            operation.PrivousValue = entity.previosValue!;
            operation.NewValue = entity.newValue!;
            operation.Cause = entity.cause!;
            operation.Cause = entity.cause!;

           _context.OperationChanges.Update(operation);
           var result = await _context.SaveChangesAsync();
            return result > 0;

        }
    }
}
