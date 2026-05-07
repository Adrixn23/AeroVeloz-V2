using AeroVeloz.Application.Repositories.Airport;
using AeroVeloz.Domain.Common.Exceptions;
using AeroVeloz.Domain.Entities.Organization.Airports;
using AeroVeloz.Domain.Models.Airports;
using AeroVeloz.Domain.Services.Interfaces.Airport;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using System.Security.Cryptography;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.Airport
{
    public class AirportRepository : IAirportRepository, IDomainServiceAirport
    {

        private readonly AeroVelozContext _context;
        private readonly ILogger<AirportRepository> _logger;

        public AirportRepository(AeroVelozContext context, ILogger<AirportRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        //metodo para validar si el aeropuerto tiene connectines pendientes de aceptar para x aerolinea o ya aceptadas

        public async Task<bool> AirportHasAirlineConnectionAsync(string airportCode, string airlineCode)
        {
            if (airportCode == null || airlineCode == null)
                return false;

            var connections = await _context.ConectionsAirlineAirports.FirstOrDefaultAsync(con => 
                  con.codeAirportIcao == airportCode && con.codeAirlinesIcao == airlineCode);
            if (connections == null) return false;
            return true;
        }

        // crear entidad de vuelo y organizacion corrrespondiente guardando los datos en la base de datos de manera simultanea
        public async Task<bool> CreateEntity(Domain.Entities.Organization.Airports.Airport entity)
        {       
            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    _context.Airports.Add(entity);
                    var result = await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                    return result > 0;
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError(ex, "Error al crear la entidad de aeropuerto. Problemas de actualización en la base de datos.");
                    throw new DatabaseOperationException("No se pudo persistir el aeropuerto debido a un error de base de datos.", ex);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error inesperado al crear la entidad de aeropuerto.");
                    throw new DatabaseOperationException("Error inesperado en la base de datos al crear el aeropuerto.", ex);
                }
            });
        }

        //desactivar la organizacion correspondiente 
        public async Task<bool> DeleteEntity(Domain.Entities.Organization.Airports.Airport entity)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var airportOrg = await _context.Organizations.Where(org =>  org.Id  == entity.Id)
                        .ExecuteUpdateAsync(setters => setters
                        .SetProperty(or => or.isActived, false)
                        );

                    await transaction.CommitAsync();
                    return airportOrg > 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al desactivar la entidad de aeropuerto {Id}.", entity.Id);
                    throw new DatabaseOperationException("No se pudo desactivar el aeropuerto.", ex);
                }
            });
        }


        //verificar si el aeropuerto existe dentro de la organizacion, si bien un aeropuerto esta asociado en una organizacion
        //este metodo ayuda a validar si se creo un registro de manera malitencionada digase directamente en la base de datos
        //o por medio de elementos fradulentos, este metodo puedes veridicar si ese aeropuerto entonces esta asociado a una organizacion 
        //o si existe esa organizacion y mo el aeropuerto.

        public async Task<bool> ExistAirportByOrganizations(string? codeIata, string? codeIacao)
        {
            var airport = await _context.Airports.FirstOrDefaultAsync(airp => airp.codeAirportIcao  == 
                codeIacao && airp.codeAirportIata  == codeIata
            );

            if (airport == null) return false;

            var org = await _context.Organizations.FirstOrDefaultAsync(org => org.Id == airport.Id);
            return org != null;
        }

        //metodo para generar api key de comunicacion del aeropuerto de manera segura

        public async Task<bool> GenerateApiKey(string? codeAirport)
        {
            var randomKey = RandomNumberGenerator.GetBytes(32);
            var apiKey = Convert.ToBase64String(randomKey);

            var airport = await _context.Airports.Where(air => air.codeAirportIcao == codeAirport)
                .ExecuteUpdateAsync(setters => setters
                      .SetProperty(a => a.apiKeyMaster, apiKey)
                );
                return airport > 0;
         
        }

        //obtener un aeropuerto  y sus datos 
        public async Task<AirportModel> GetAirportByCode(string? codeAirport)
        {
            var airport = await(
                 from a in _context.Airports.AsNoTracking()
                 join o in _context.Organizations.AsNoTracking()
                  on a.Id equals o.Id
                 where a.codeAirportIcao == codeAirport
                 select new AirportModel(
                      a.Id,
                      a.codeAirportIcao,
                      a.codeAirportIata,
                      o.nameOrganization,
                      a.timeOffset,
                      a.city,
                      a.country,
                      o.emailOrganization,
                      o.isActived,
                      a.apiKeyMaster
                     )).FirstOrDefaultAsync();

            if (airport != null)
                return airport;

            return null!;
        }

       

        public async Task<IReadOnlyCollection<AirportModel>> GetAllAirport()
        {
            var airports = await (
                    from air in _context.Airports.AsNoTracking()
                    join or in  _context.Organizations.AsNoTracking()
                        on air.Id equals or.Id
                    select new AirportModel(
                          air.Id,
                          air.codeAirportIcao,
                          air.codeAirportIata,
                          or.nameOrganization,
                          air.timeOffset,
                          air.city,
                          air.country,
                          or.emailOrganization,
                          or.isActived,
                          air.apiKeyMaster
                        )
                ).ToListAsync();
            if(!airports.Any()) return Array.Empty<AirportModel>();
            return airports;
        }

       

        public async Task<bool> UpdateEntity(Domain.Entities.Organization.Airports.Airport entity)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var org = await _context.Organizations.Where(or => or.Id == entity.Id).
                        ExecuteUpdateAsync(setters => setters 
                        .SetProperty(e => e.isActived, entity.isActived)
                        .SetProperty(e => e.typeOrganization, entity.typeOrganization)
                        .SetProperty(e => e.emailOrganization, entity.emailOrganization)
                        .SetProperty(e => e.nameOrganization, entity.nameOrganization)
                        );

                    var airport = await _context.Airports.Where(air =>
                    air.Id == entity.Id
                    ).ExecuteUpdateAsync(
                        setters => setters
                        .SetProperty(a => a.codeAirportIcao, entity.codeAirportIcao )
                        .SetProperty(a => a.codeAirportIata, entity.codeAirportIata )
                        .SetProperty(a => a.city, entity.city)
                        .SetProperty(a => a.country, entity.country)
                        .SetProperty(a => a.timeOffset, entity.timeOffset)
                        );

                    await transaction.CommitAsync();
                    return org > 0 && airport > 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error actualizando la entidad de aeropuerto con ID: {Id}", entity.Id);
                    throw new DatabaseOperationException("Ocurrió un error en la base de datos al actualizar el aeropuerto.", ex);
                }
            });
        }
    }
}
