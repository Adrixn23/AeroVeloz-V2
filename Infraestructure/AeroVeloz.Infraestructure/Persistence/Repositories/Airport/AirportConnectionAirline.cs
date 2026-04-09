using AeroVeloz.Application.Repositories.Airport;
using AeroVeloz.Domain.Entities.Organization.Airport;
using AeroVeloz.Domain.Common.Exceptions;
using AeroVeloz.Domain.Models.Airports;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.Airport
{
    public class AirportConnectionAirline : IAirportConnectionAirline
    {

        private readonly AeroVelozContext _context;
        private readonly ILogger<AirportConnectionAirline> _logger;

        public AirportConnectionAirline(AeroVelozContext context, ILogger<AirportConnectionAirline> logger) {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreateEntity(ConectionsAirlineAirport entity)
        {
            try
            {
                var connection =  await _context.ConectionsAirlineAirports.AddAsync(entity);
                var result = await _context.SaveChangesAsync();

                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la conexión aerolínea-aeropuerto");
                throw new DatabaseOperationException("Error persistiendo la conexión aerolínea-aeropuerto en base de datos", ex);
            }
        }

        public async Task<bool> DeleteEntity(ConectionsAirlineAirport entity)
        {
            try
            {
                var connection = await _context.ConectionsAirlineAirports.Where(con => con.Id == entity.Id).ExecuteUpdateAsync(setters => setters
                   .SetProperty(c => c.isActive, false));

                    return connection > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al desactivar (eliminar) la conexión {Id}", entity.Id);
                throw new DatabaseOperationException($"Error desactivando la conexión con Id: {entity.Id}", ex);
            }
        }

        public async Task<IReadOnlyCollection<AirlineConnectionByAirportModel>> GetAirportConnectionById(string? codeAirportIcao)
        {
            try
            {
                var conections = await(
                    from c in _context.ConectionsAirlineAirports.AsNoTracking()
                                 join a in _context.Airlines.AsNoTracking()
                                     on c.codeAirlinesIcao equals a.codeAirlinesIcao into aGroup
                                 from a in aGroup.DefaultIfEmpty()
                                 where c.codeAirportIcao  == codeAirportIcao
                                 select new AirlineConnectionByAirportModel(
                                          c.Id,
                                          c.codeAirportIcao,
                                          c.codeAirlinesIcao,
                                          a != null ? a.nameOrganization : "Desconocida",
                                          c.isActive,
                                          c.createAt
                                     )).ToListAsync();

                if(conections.Any())
                    return  conections;

                return Array.Empty<AirlineConnectionByAirportModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consultando conexiones por código de aeropuerto {Code}", codeAirportIcao);
                throw new DatabaseOperationException($"Error consultando conexiones por código de aeropuerto {codeAirportIcao}", ex);
            }
        }

        public async Task<bool> UpdateEntity(ConectionsAirlineAirport entity)
        {
            try
            {
                var connections = await _context.ConectionsAirlineAirports.Where(con => con.Id == entity.Id).ExecuteUpdateAsync(setters => setters
                    .SetProperty(c =>  c.codeAirlinesIcao,  entity.codeAirlinesIcao)
                    .SetProperty(c => c.codeAirportIcao, entity.codeAirportIcao)
                    .SetProperty(c => c.isActive, entity.isActive)
                    .SetProperty(c => c.tokenApi, entity.tokenApi)
                );

                return connections > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la conexión aerolínea-aeropuerto {Id}", entity.Id);
                throw new DatabaseOperationException($"Error actualizando la conexión aerolínea-aeropuerto con Id: {entity.Id}", ex);
            }
        }

        public async Task<ConectionsAirlineAirport?> GetConnectionByIdAsync(Guid connectionId)
        {
            try
            {
                return await _context.ConectionsAirlineAirports
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == connectionId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consultando conexión por Id {Id}", connectionId);
                throw new DatabaseOperationException($"Error consultando conexión con Id: {connectionId}", ex);
            }
        }
    }
}
