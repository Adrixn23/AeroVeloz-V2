using AeroVeloz.Application.Repositories.Airport;
using AeroVeloz.Domain.Entities.Organization.Airport;
using AeroVeloz.Domain.Models.Airports;
using AeroVeloz.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.Airport
{
    public class AirportConnectionAirline : IAirportConnectionAirline
    {

        private readonly AeroVelozContext _context;
        public AirportConnectionAirline(AeroVelozContext context) {
            _context = context;
        }

        public async Task<bool> CreateEntity(ContectionsAirlineAirport entity)
        {
            var con = new AeroVeloz.Infraestructure.Persistence.Entities.ConectionsAirlineAirport
            {
                IdConection = entity.Id,
                CodeAirlines = entity.codeAirlines!,
                CodeAirport = entity.codeAirport!
            };

            await _context.AddAsync(con);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteEntity(ContectionsAirlineAirport entity)
        {

            var result = await _context.ConectionsAirlineAirports.Where(en => en.IdConection == entity.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(u => u.IsActive, false)
                );

            return result > 0;
        }

        public async Task<IReadOnlyCollection<AirlineConnectionByAirportModel>> GetAirportConnectionById(string? codeAirportIata, string? codeAirportIcao)
        {
            var airport = await _context.Airports.FirstOrDefaultAsync(air => air.CodeIata.ToLower().Trim()
            == codeAirportIata!.ToLower().Trim() && air.CodeAirport.ToLower().Trim() == codeAirportIcao);

            if(airport != null && airport.IdOrganizationNavigation.IsActive != false)
            {
                var connection = _context.ConectionsAirlineAirports.Where(co =>
                    co.CodeAirportNavigation.CodeIata.ToLower() == codeAirportIata!.ToLower().Trim() &&
                    co.CodeAirportNavigation.CodeAirport.ToLower().Trim() ==
                    codeAirportIcao!.ToLower().Trim())
                    .Select( air =>
                        new AirlineConnectionByAirportModel(
                            air.CodeAirport,
                            air.CodeAirlines,
                            air.IsActive ?? false,
                            air.CreateAt.GetValueOrDefault(),
                            air.TokenApi
                        )
                    );

                return await connection.ToListAsync();
            }
            return Array.Empty<AirlineConnectionByAirportModel>();
        }

        public async Task<bool> UpdateEntity(ContectionsAirlineAirport entity)
        {
            var collectionAirline = await _context.ConectionsAirlineAirports.Where(con => con.IdConection == entity.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(c => c.CodeAirlines, entity.codeAirlines)
                .SetProperty(c => c.CodeAirport, entity.codeAirport)
                .SetProperty(c => c.IsActive, entity.isActive)
                .SetProperty(c => c.TokenApi, entity.tokenApi)
                );

            return collectionAirline > 0;
        }
    }
}
