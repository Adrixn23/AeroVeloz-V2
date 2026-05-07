using AeroVeloz.Application.Repositories.Airlines;
using AeroVeloz.Domain.Entities.Airlines;
using AeroVeloz.Domain.Models.Airline;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.EntityFrameworkCore;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.Airlines
{
    public class AirlineRepository : IAirlineRepository
    {
        private readonly AeroVelozContext _context;

        public AirlineRepository(AeroVelozContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateEntity(Airline entity)
        {
            _context.Airlines.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateEntity(Airline entity)
        {
            _context.Airlines.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteEntity(Airline entity)
        {
            var result = await _context.Airlines
           .Where(a => a.Id == entity.Id)
           .ExecuteUpdateAsync(s => s.SetProperty(a => a.isActived, false));
            return result > 0;
        }

        public async Task<Airline?> GetEntityByCodeAsync(string codeAirlinesIcao)
        {
            return await _context.Airlines
                .FirstOrDefaultAsync(a => a.codeAirlinesIcao == codeAirlinesIcao);
        }

        public async Task<Airline?> GetEntityByCodeNoTrackingAsync(string codeAirlinesIcao)
        {
            return await _context.Airlines
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.codeAirlinesIcao == codeAirlinesIcao);
        }

        public async Task<AirlineDetailModel?> GetDetailByCodeAsync(string codeAirlinesIcao)
        {
            return await _context.Airlines
                .AsNoTracking()
                .Where(a => a.codeAirlinesIcao == codeAirlinesIcao)
                .Select(a => new AirlineDetailModel(
                    a.Id,
                    a.codeAirlinesIcao!,
                    a.codeIata!,
                    a.nameOrganization!,
                    a.isActived,
                    a.createAt
                ))
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsByCodeAsync(string codeAirlinesIcao)
        {
            return await _context.Airlines
                .AsNoTracking()
                .AnyAsync(a => a.codeAirlinesIcao == codeAirlinesIcao);
        }

        public async Task<IReadOnlyCollection<AirlineDetailModel>> GetAllActiveDetailsAsync()
        {
            return await _context.Airlines
                .AsNoTracking()
                .Where(a => a.isActived)
                .Select(a => new AirlineDetailModel(
                    a.Id,
                    a.codeAirlinesIcao!,
                    a.codeIata!,
                    a.nameOrganization!,
                    a.isActived,
                    a.createAt
                ))
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<Airline>> GetAllActiveEntitiesAsync()
        {
            return await _context.Airlines
                .AsNoTracking()
                .Where(a => a.isActived)
                .ToListAsync();
        }
    }
}