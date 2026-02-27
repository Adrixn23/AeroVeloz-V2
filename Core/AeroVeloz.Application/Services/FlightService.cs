using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AeroVeloz.Application.DTOs;

namespace AeroVeloz.Application.Interfaces
{
    public interface IFlightService
    {
        //  Crear un vuelo validando aerolínea e integridad
        Task<FlightResponseDto> CreateFlightAsync(FlightCreateDto flightDto);

        // Procesar el lote de la aerolinea
        Task<BatchResponseDto> ProcessFlightBatchAsync(IEnumerable<FlightCreateDto> batch);

        // Actualizar estadoo
        Task<FlightResponseDto> UpdateStatusAsync(short flightId, byte newStateId, string airlineCode);

        //  Obtener el tablero operativo actualizado
        Task<IEnumerable<FlightResponseDto>> GetOperationalBoardAsync();
    }
}
