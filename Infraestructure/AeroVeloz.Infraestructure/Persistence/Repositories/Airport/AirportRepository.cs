using AeroVeloz.Application.Repositories.Airport;
using AeroVeloz.Domain.Entities.Organization.Airports;
using AeroVeloz.Domain.Models.Airports;
using AeroVeloz.Domain.Services.Interfaces.Airport;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.Airport
{
    public class AirportRepository : IAirportRepository, IDomainServiceAirport
    {

        private readonly AeroVelozContext _context;

        public AirportRepository(AeroVelozContext context)
        {
            _context = context;
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
            _context.Airports.Add(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
         
        }

        //desactivar la organizacion correspondiente 
        public async Task<bool> DeleteEntity(Domain.Entities.Organization.Airports.Airport entity)
        {
            var airportOrg = await _context.Organizations.Where(org =>  org.Id  == entity.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(or => or.isActived, false)
                );
   
            var reult = await _context.SaveChangesAsync();
            return reult > 0;
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
                      a.codeAirportIcao,
                      a.codeAirportIata,
                      o.nameOrganization,
                      a.timeOffset,
                      a.city,
                      a.country
                     )).FirstOrDefaultAsync();

            if (airport != null)
                return airport;

            return null!;
        }

        //obtener todos los aeropuertos del sistema
        //tomando en cuenta que este metodo opera bajo el elemento de authorization para validar si el usuario es del tipo SYSTEM_ADMIN
        //sino lo es ni podra realizar esta operacion 

        public async Task<IReadOnlyCollection<AirportModel>> GetAllAirport()
        {
            var airports = await (
                    from air in _context.Airports.AsNoTracking()
                    join or in  _context.Organizations.AsNoTracking()
                        on air.Id equals or.Id
                    select new AirportModel(
                          air.codeAirportIcao,
                          air.codeAirportIata,
                          or.nameOrganization,
                          air.timeOffset,
                          air.city,
                          air.country
                        )
                ).ToListAsync();
            if(!airports.Any()) return Array.Empty<AirportModel>();
            return airports;
        }

        //modificacion total de los diversos campos previamente registrados de un aeropuerto
        //tanto  en su tabla de aeropuerto como en organization 

        public async Task<bool> UpdateEntity(Domain.Entities.Organization.Airports.Airport entity)
        {
            var org = await _context.Organizations.Where(or => or.Id == entity.Id).
                ExecuteUpdateAsync(setters => setters 
                .SetProperty(e => e.isActived, entity.isActived)
                .SetProperty(e => e.typeOrganization, entity.typeOrganization)
                .SetProperty(e => e.emailOrganization, entity.emailOrganization)
                .SetProperty(e => e.nameOrganization, entity.nameOrganization)
                );
            var airport = await _context.Airports.Where(air =>
               air.codeAirportIcao == entity.codeAirportIcao
            ).ExecuteUpdateAsync(
                setters => setters
                .SetProperty(a => a.city, entity.city)
                .SetProperty(a => a.apiKeyMaster, entity.apiKeyMaster)
                .SetProperty(a => a.country, entity.country)
                .SetProperty(a => a.timeOffset, entity.timeOffset)
                );

            return org > 0 && airport > 0;

        }
    }
}
