using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Entities.BaseEntity;
<<<<<<< HEAD
using AeroVeloz.Domain.Entities.Flight;
using AeroVeloz.Domain.Entities.Security;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
=======
>>>>>>> feature/domain-model

namespace AeroVeloz.Domain.Entities.Operations;

public partial class OperationChange :  BEntity<Guid>
{
      public int  idUser { get; private set; }  
      public OperationalChangeType operationalChangeType { get; private set; }
      public string? codeAirline { get; private set; }
      public int flightNumber { get; private set; }
      
      /*
       * Descomentar este elemento cuando el otro developer (adrian) cree el enum de los estados de vuelos
       * con su respectivo valor de codigo para cada uno 
       *
       *public FlightState previousValue {get; private set; } 
       public FlightState newValue {get; private set; } 
       */
      public DateTime changeAt { get; private set; }
      public string? cause { get; private set; }

<<<<<<< HEAD
    
=======
    private OperationChange(Guid id,int idUser, OperationalChangeType operationalChangeType, string codeAirline, int flightNumber
         /*FlightState previousValue, FlightState newValue */)
    {
        this.Id = id;
        this.operationalChangeType = operationalChangeType;
        this.codeAirline = codeAirline;
        this.flightNumber = flightNumber;
        /*this.previousValue = previosValue;
         this.newValue = newValue;
         */
    }
>>>>>>> feature/domain-model

}
