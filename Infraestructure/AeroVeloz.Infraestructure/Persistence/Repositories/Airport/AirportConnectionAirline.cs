using AeroVeloz.Application.Repositories.Airport;
using AeroVeloz.Domain.Entities.Organization.Airport;
using AeroVeloz.Domain.Models.Airports;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.EntityFrameworkCore;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.Airport
{
    public class AirportConnectionAirline : IAirportConnectionAirline
    {

        private readonly AeroVelozContext _context;
        public AirportConnectionAirline(AeroVelozContext context) {
            _context = context;
        }

        public async Task<bool> CreateEntity(ConectionsAirlineAirport entity)
        {

            var connection =  await _context.ConectionsAirlineAirports.AddAsync(entity);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteEntity(ConectionsAirlineAirport entity)
        {

            var connection = await _context.ConectionsAirlineAirports.Where(con => con.Id == entity.Id).ExecuteUpdateAsync(setters => setters
               .SetProperty(c => c.isActive, false));
              
                return connection > 0;
          
        }

        public async Task<IReadOnlyCollection<AirlineConnectionByAirportModel>> GetAirportConnectionById(string? codeAirportIcao)
        {

            var conections = await(
                from c in _context.ConectionsAirlineAirports.AsNoTracking()
                             join a in _context.Airlines.AsNoTracking()
                                 on c.codeAirlinesIcao equals a.codeAirlinesIcao
                             join or in _context.Organizations.AsNoTracking()
                                  on a.Id equals or.Id
                                   where c.codeAirportIcao  == codeAirportIcao
                             select new AirlineConnectionByAirportModel(
                                      c.codeAirportIcao,
                                      c.codeAirlinesIcao,
                                     or.nameOrganization,
                                     c.isActive,
                                     c.createAt

                                 )).ToListAsync();

            if(conections.Any())
                return  conections;

            return Array.Empty<AirlineConnectionByAirportModel>();
        }

        public async Task<bool> UpdateEntity(ConectionsAirlineAirport entity)
        {

            var connections = await _context.ConectionsAirlineAirports.Where(con => con.Id == entity.Id).ExecuteUpdateAsync(setters => setters
                .SetProperty(c =>  c.codeAirlinesIcao,  entity.codeAirlinesIcao)
                .SetProperty(c => c.codeAirportIcao, entity.codeAirportIcao)
                .SetProperty(c => c.isActive, entity.isActive)
                .SetProperty(c => c.tokenApi, entity.tokenApi)
            );
         
            return connections > 0;
        }
    }
}
