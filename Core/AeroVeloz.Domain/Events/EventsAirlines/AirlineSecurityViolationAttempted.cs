using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroVeloz.Domain.Events.EventsAirlines
{

    /* (Refer sad Página 38 segurida Control de acceso por rolee y Página 13 (Módulo de Trazabilidad)
Este evento registra cualquier intento de una aerolínea de modificar vuelos que no le pertenecen, garantizando la
integridad de los datos según el diseño de seguridad.*/


    public record AirlineSecurityViolationAttempted (

       string AttemptedByAirlineCode,
       string TargetFlightNumber,
       string Operation,
         DateTime DetectedAt

        ) : INotification;
    
    
}
