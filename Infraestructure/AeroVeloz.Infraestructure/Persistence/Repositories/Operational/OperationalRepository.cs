using AeroVeloz.Application.Repositories.Operational;
using AeroVeloz.Domain.Entities.Operations;
using AeroVeloz.Domain.Models.Operational;
using AeroVeloz.Domain.Services.Interfaces.Operational;
using AeroVeloz.Infraestructure.Persistence.context;
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
          
            _context.OperationChanges.Add(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
            
        }

        public async Task<bool> DeleteEntity(OperationChange entity)
        {
            var op = await _context.OperationChanges.Where(p => p.Id == entity.Id)
               .ExecuteUpdateAsync(setters => setters
                 .SetProperty(op => op.isActive, entity.isActive)
               );
            return op > 0;
        }

        public async Task<IReadOnlyCollection<OperationalDetailModel>> GetAirportChangesAsync(int orgId)
        {

            var operationsByAirport = await (
                from op in _context.OperationChanges.AsNoTracking()
                join or in _context.Organizations.AsNoTracking()
                on orgId equals or.Id
                join air in _context.Airports.AsNoTracking()
                on orgId equals air.Id
                join opt in _context.OperationalChangeTypes.AsNoTracking()
                on op.idOperationalType equals opt.Id
                where op.codeAirportIcao == air.codeAirportIcao
                select new OperationalDetailModel(op.idUser, op.Id, or.nameOrganization, opt.name, op.changeAt, op.cause )

                ).ToListAsync();
            if(operationsByAirport.Any()) 
                return operationsByAirport;

            return Array.Empty<OperationalDetailModel>();

        }

        public async Task<OperationalModel> GetByOperationAsync(Guid operationId)
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

        public async Task<IReadOnlyCollection<OperationalModel>> GetFlightChangesAsync(short flightNumber)
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

        public async Task<bool> OperationAlreadyRegisteredAsync(Guid operationId, short typeOp)
        {
            var operation = await _context.OperationChanges.FirstOrDefaultAsync(op => op.Id == operationId);
            if(operation == null) return false;
            if (operation.idOperationalType == typeOp) return false;
       
            return true;
        }

        public async Task<bool> OperationBelongsToOrganizationAsync(Guid operationId, int organizationId)
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

        public async Task<bool> OperationConsultFlightValid(short flightNumber)
        {

            var fl = await _context.Flights.FirstOrDefaultAsync(f => f.Id == flightNumber);
            if (fl == null) return false;
            if (DateTimeOffset.UtcNow - fl.ScheduledDeparture > TimeSpan.FromDays(2)) return false;
            var stateF = await _context.FlightStates.FirstOrDefaultAsync(fs => fs.Id == fl.flightStatesId);
            if(stateF == null) return false;
            if(stateF.name == "CANCELLED" || stateF.name == "InFlight") return false;
            return true;
        }

        public async Task<bool> OperationExistsAsync(Guid operationId)
        {
            var operation = await _context.OperationChanges.FirstOrDefaultAsync(op => op.Id  == operationId);
            return operation != null;
        }

        public async Task<bool> UpdateEntity(OperationChange entity)
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

        public async Task<string?> GetOperationalTypeNameAsync(short typeId)
        {
            var type = await _context.OperationalChangeTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == typeId);
            return type?.name;
        }
    }
}
