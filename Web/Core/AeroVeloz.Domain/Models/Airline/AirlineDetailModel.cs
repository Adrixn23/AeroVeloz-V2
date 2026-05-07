namespace AeroVeloz.Domain.Models.Airline;
    
  
     //Modelo de dominio para representar el detalle de una aerolínea.
    //Se usa en el repositorio para proyecciones rápidas
    
   public record AirlineDetailModel(
        int Id,
       string codeAirlinesIcao,
       string codeIata,
       string nameOrganization,
       bool isActive,
       DateTime createAt
   );