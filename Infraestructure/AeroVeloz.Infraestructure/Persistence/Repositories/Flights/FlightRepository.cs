
using AeroVeloz.Infraestructure.Persistence.context;
namespace AeroVeloz.Infraestructure.Persistence.Repositories.Flights
{
    public class FlightsRepository /*: IFlightsRepository*/ // tanto la interfaz de iflightrepository y iflightdomainService,
                                                            // pasar por inyeccion de dependencias las politicas y las interfaces de las politicas
    {
        private readonly AeroVelozDbContext _context;

        public FlightsRepository(AeroVelozDbContext context)
        {
            _context = context;
        }

       
    }
}