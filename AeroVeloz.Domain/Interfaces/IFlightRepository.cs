using AeroVeloz.Domain.Entities.Flight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroVeloz.Domain.Interfaces
{
    public interface IFlightRepository 
    {
       Task<Flight?> GetByIdAsync(short id); // Recibe el short id, esto Permite recuperar el vuelo para auditar su historial o cambiar su estado


        Task<Flight> AddAsync(Flight flight); // para agregar un nuevo vuelo a la base de datos 


        Task<Flight> UpdateAsync(Flight flight); // Para actualizar un vuelo, esto puede ser util para cambios de puerta o estado 


        Task<bool> ExistsAsync(string codeAirlines); // es para verificar si un vuelo realmente ya existe, para evitar duplicados


        Task<IEnumerable<Flight>> GetActiveFlightsAsync(); // esto devuelve una coleccion de objeto(vuelos) cone sto se puede filtrar por estado
                                                            // por ejemplo, abordado, abarcado, en vuelo, etc. y tambien se puede filtrar por fecha, ejm que solo pueda
                                                            // traer un vuelo,solo con la fecha de hoy. 

    }
}
