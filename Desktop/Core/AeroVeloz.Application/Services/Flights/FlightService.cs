using AeroVeloz.Application.Contracts.Flights;
using AeroVeloz.Application.DTOs.Flights;
using AeroVeloz.Application.Repositories.Flights;
using AeroVeloz.Application.Handlers.Result;
using AeroVeloz.Transversal.Contracts.Monitoring;
using Microsoft.Extensions.Logging;

namespace AeroVeloz.Application.Services.Flights
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly ILogger<FlightService> _logger;
        private readonly IOrganizationMonitoringLogger _monitoringLogger;

        public FlightService(
            IFlightRepository flightRepository,
            ILogger<FlightService> logger,
            IOrganizationMonitoringLogger monitoringLogger)
        {
            _flightRepository = flightRepository;
            _logger = logger;
            _monitoringLogger = monitoringLogger;
        }

        public async Task<OperationResult<IReadOnlyCollection<FlightDetailsDto>>> GetAllActiveFlightsAsync(Guid userId, int orgId)
        {
            try
            {
                var flights = await _flightRepository.GetAllActiveFlightsWithDetailsAsync();

                if (flights == null || flights.Count == 0)
                {
                    return OperationResult<IReadOnlyCollection<FlightDetailsDto>>.Ok(
                        Array.Empty<FlightDetailsDto>(),
                        "No hay vuelos activos disponibles");
                }

                return OperationResult<IReadOnlyCollection<FlightDetailsDto>>.Ok(
                    flights,
                    "Vuelos obtenidos exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo todos los vuelos activos para el usuario {UserId}", userId);
                return OperationResult<IReadOnlyCollection<FlightDetailsDto>>.Fail(
                    "FLIGHTS_RETRIEVAL_ERROR",
                    "Error al obtener los vuelos activos");
            }
        }

        public async Task<OperationResult<IReadOnlyCollection<FlightDetailsDto>>> GetFlightsByAirportAsync(string airportCode, Guid userId, int orgId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(airportCode))
                {
                    return OperationResult<IReadOnlyCollection<FlightDetailsDto>>.Fail(
                        "INVALID_AIRPORT_CODE",
                        "El código del aeropuerto no es válido");
                }

                var flights = await _flightRepository.GetFlightsByAirportWithDetailsAsync(airportCode);

                if (flights == null || flights.Count == 0)
                {
                    return OperationResult<IReadOnlyCollection<FlightDetailsDto>>.Ok(
                        Array.Empty<FlightDetailsDto>(),
                        $"No hay vuelos para el aeropuerto {airportCode}");
                }

                return OperationResult<IReadOnlyCollection<FlightDetailsDto>>.Ok(
                    flights,
                    "Vuelos del aeropuerto obtenidos exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo vuelos por aeropuerto {AirportCode}", airportCode);
                return OperationResult<IReadOnlyCollection<FlightDetailsDto>>.Fail(
                    "FLIGHTS_BY_AIRPORT_ERROR",
                    "Error al obtener los vuelos del aeropuerto");
            }
        }

        public async Task<OperationResult<FlightDetailsDto>> GetFlightDetailsAsync(short flightId, Guid userId, int orgId)
        {
            try
            {
                var flight = await _flightRepository.GetFlightWithDetailsAsync(flightId);

                if (flight == null)
                {
                    return OperationResult<FlightDetailsDto>.Fail(
                        "FLIGHT_NOT_FOUND",
                        $"El vuelo con ID {flightId} no existe");
                }

                return OperationResult<FlightDetailsDto>.Ok(
                    flight,
                    "Detalles del vuelo obtenidos exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo detalles del vuelo {FlightId}", flightId);
                return OperationResult<FlightDetailsDto>.Fail(
                    "FLIGHT_DETAILS_ERROR",
                    "Error al obtener los detalles del vuelo");
            }
        }
    }
}
