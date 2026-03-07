using AeroVeloz.Application.Repositories.Airport;
using AeroVeloz.Domain.Models.Airports;
using AeroVeloz.Domain.Services.Interfaces.Airport;
using AeroVeloz.Infraestructure.Persistence.Context;
using AeroVeloz.Infraestructure.Persistence.Entities;
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
                con.CodeAirport.ToLower().Trim() == airportCode.ToLower().Trim()  
                && con.CodeAirlines.ToLower().Trim() == airlineCode.ToLower().Trim()
            );
            if (connections == null) return false;
            return true;
        }


        // crear entidad de vuelo y organizacion corrrespondiente guardando los datos en la base de datos de manera simultanea
        public async Task<bool> CreateEntity(Domain.Entities.Organization.Airports.Airport entity)
        {
          
            var org = new Organization
            {
                TypeOrganization =  entity.typeOrganization.ToString(),
                EmailOrganizations = entity.emailOrganization!,
                NameOrganization = entity.nameOrganization!
            };

            _context.Add(org); 

            var airpor = new Entities.Airport
            {
                CodeAirport = entity.codeAirportIcao!,
                City  = entity.city!,
                Country = entity.country!,
                ApiKeyMaster = "NOT_API_KEY_GENERE",
                TimeZone =  entity.timeOffset,
                CodeIata = entity.codeAirportIata!,
                IdOrganization = org.IdOrganizations
            };

            var result = await _context.SaveChangesAsync();
            return result > 0;
         
        }

        //desactivar la organizacion correspondiente 
        public async Task<bool> DeleteEntity(Domain.Entities.Organization.Airports.Airport entity)
        {
            var airportOrg = await _context.Organizations.Where(org => org.IdOrganizations == entity.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(or => or.IsActive, false)
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
            var airport = await _context.Airports.FirstOrDefaultAsync(airp => airp.CodeAirport.ToLower().Trim() == 
                codeIacao!.ToLower().Trim() && airp.CodeIata.ToLower().Trim() == codeIata!.ToLower().Trim()
            );

            var org = await _context.Organizations.FirstOrDefaultAsync(org => org.IdOrganizations == airport!.IdOrganization);
            if(airport != null && org != null) return true;
            return false;

        }

        //metodo para generar api key de comunicacion del aeropuerto de manera segura
        public async Task<bool> GenerateApiKey(string? codeAirport)
        {
            var airport =  await _context.Airports.FirstOrDefaultAsync(air => air.CodeAirport.ToLower().Trim() == codeAirport!.ToLower().Trim());

            if(airport != null)
            {

                var randomKey = RandomNumberGenerator.GetBytes(32);
                var apiKey = Convert.ToBase64String(randomKey);
                airport.ApiKeyMaster = apiKey;
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }

            return false;
        }

        //obtener un aeropuerto  y sus datos 
        public async Task<AirportModel> GetAirportByCode(string? codeAirport)
        {
            var airport = await _context.Airports.FirstOrDefaultAsync(air =>
            air.CodeAirport.ToLower().Trim() == codeAirport!.ToLower().Trim());

            if(airport != null)
            {
                return new AirportModel(
                        airport.CodeAirport,
                        airport.CodeIata,
                        airport.IdOrganizationNavigation.NameOrganization,
                        airport.TimeZone,
                        airport.City,
                        airport.Country
                    );
            }

            return null!;
        }

        //obtener todos los aeropuertos del sistema
        //tomando en cuenta que este metodo opera bajo el elemento de authorization para validar si el usuario es del tipo SYSTEM_ADMIN
        //sino lo es ni podra realizar esta operacion 

        public async Task<IReadOnlyCollection<AirportModel>> GetAllAirport()
        {
            var airports = await _context.Airports.Select(
                    air => new AirportModel(
                        air.CodeAirport,
                        air.CodeIata,
                        air.IdOrganizationNavigation.NameOrganization,
                        air.TimeZone,
                        air.City,
                        air.Country
                        )
                ).ToListAsync();

            if(!airports.Any()) return Array.Empty<AirportModel>();
            return airports;
        }

        //modificacion total de los diversos campos previamente registrados de un aeropuerto
        //tanto  en su tabla de aeropuerto como en organization 
        public async Task<bool> UpdateEntity(Domain.Entities.Organization.Airports.Airport entity)
        {
            var reuslt = await _context.Airports.Where(air => air.CodeAirport.ToLower().Trim() ==
                   entity.codeAirportIcao!.ToLower().Trim() && air.CodeIata == entity.codeAirportIata!.ToLower().Trim()
            ).ExecuteUpdateAsync(setters => setters
                   .SetProperty(a => a.CodeAirport, entity.codeAirportIcao)
                   .SetProperty(a => a.CodeIata, entity.codeAirportIata)
                   .SetProperty(a => a.City, entity.city)
                   .SetProperty(a => a.Country, entity.country)
                   .SetProperty(a => a.TimeZone, entity.timeOffset)
                   .SetProperty(a => a.ApiKeyMaster, entity.apiKeyMaster)
                   .SetProperty(a => a.IdOrganizationNavigation.NameOrganization, entity.nameOrganization)
                   .SetProperty(a => a.IdOrganizationNavigation.IsActive, entity.isActived)
                   .SetProperty(a => a.IdOrganizationNavigation.TypeOrganization, entity.typeOrganization.ToString())
                   .SetProperty(a => a.IdOrganizationNavigation.EmailOrganizations, entity.emailOrganization)
            );

            return reuslt > 0;
        }
    }
}
