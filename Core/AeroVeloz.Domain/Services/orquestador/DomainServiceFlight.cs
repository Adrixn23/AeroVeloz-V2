using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.DomainService.Interfaces.Flight;
using AeroVeloz.Domain.TransitionPolices;
using AeroVeloz.Domain.TransitionPolices.interfaces.InterfacesFlightState;
using AeroVeloz.Domain.ValidationBase;
using AeroVeloz.Domain.Validators.codeError.codeError_Airlines;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// Alias para evitar conflictos
using FlightEntity = AeroVeloz.Domain.Entities.Flight.Flight;

namespace AeroVeloz.Domain.DomainService.Flights
{
    public class FlightDomainService : IDomainServiceFlight
    {
        private readonly IFlightLifeCiclyePolicy _lifeCyclePolicy;



        public FlightDomainService(IFlightLifeCiclyePolicy lifeCyclePolicy)
        {
            _lifeCyclePolicy = lifeCyclePolicy;
        }

        public async Task<FlightEntity> ChangeStatedFlightAsync(FlightEntity flight, FlightStateEnum newState)
        {
            //Validar la regla de negocio
            if (!_lifeCyclePolicy.CanTrasition(flight.FlightStated, newState))
            {
                // resultado de validación fallido para pasarlo a la excepcion
                var errorResult = new ValidationResult().Failur(ErrorAirlines.InvalidCancellationInFlight);

                
                throw new FlightDomainException($"cambio de estado invalido {newState}", errorResult);
            }

            // aqui estaba un poco confundido, Como no es un record y tiene init, creo que la unica forma se podria decir legal en C# sin tocar la entidad
            // es crear una instancia nueva si fuera posible, pero como el modelo es anemico
            // lo ideal sera que cambie init por set en la clase Flight.cs.
            // Mientras tanto si el compilador me da error acaa porque FlightStated es inmutable

            // si usara set usaria esto: 
            //flight.FlightStated = newState;

            return await Task.FromResult(flight);
        }

        public async Task<FlightEntity> ChangeBoardingFlightAsync(FlightEntity flight, string newGate)
        {
            // Regla de negocio
            if (flight.FlightStated == FlightStateEnum.AterrizadoArribado ||
                flight.FlightStated == FlightStateEnum.Cancelado ||
                flight.FlightStated == FlightStateEnum.Finalizado)
            {
                return await Task.FromResult(flight);
            }

           

            // flight.BoardingGate = newGate;

            return await Task.FromResult(flight);
        }

        public ValidationResult GetcodeAirlinesOwner(FlightEntity flight, string codeAirline)
        {
            var result = new ValidationResult();

            if (flight.codeAirlines != codeAirline)
            {
                return result.Failur(ErrorAirlines.UnauthorizedBatchAccess);
            }

            return result.Success();
        }

        public async Task<ValidationResult> IsvalidOriginAirport(FlightEntity flight)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(flight.OriginAirport))
            {
                return await Task.FromResult(result.Failur(ErrorAirlines.InvalidBatchCoherence));
            }

            return await Task.FromResult(result.Success());
        }

        public async Task<FlightEntity> GetFlightidNumber(short id) => throw new NotImplementedException(); // se necesita base de datos
        public async Task<IEnumerable<FlightEntity>> GetAllFlightsOperational() => throw new NotImplementedException(); // se necesita base de datos
    }
}