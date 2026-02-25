using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Entities.BaseEntity;
using AeroVeloz.Domain.Entities.Flights;
using AeroVeloz.Domain.Entities.Security;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AeroVeloz.Domain.Entities.Operations;

public partial class OperationChange :  BEntity<Guid>
{
      public int  idUser { get; private set; }  
      public OperationalChangeType operationalChangeType { get; private set; }
      public char? codeAirline { get; private set; }
      public int flightNumber { get; private set; }
      
      /*
       * Descomentar este elemento cuando el otro developer (adrian) cree el enum de los estados de vuelos
       * con su respectivo valor de codigo para cada uno 
       *
       * public FlightState previousValue {get; private set; } 
       public FlightState newValue {get; private set; } 

       */
      public DateTime changeAt { get; private set; }
      public string? cause { get; private set; }

    private OperationChange(Guid id,int idUser, OperationalChangeType operationalChangeType, char codeAirline, int flightNumber
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

}
