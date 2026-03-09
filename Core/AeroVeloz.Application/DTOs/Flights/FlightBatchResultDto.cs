namespace AeroVeloz.Application.DTOs.Flights
{
    public sealed record FlightBatchResultDto(
        int TotalReceived,
        int TotalPersisted,
        int TotalRejected,
        IReadOnlyCollection<FlightBatchErrorDto> Errors
    );

    public sealed record FlightBatchErrorDto(
        int RowIndex,
        string? CodeAirlines,
        string? ErrorCode,
        string? ErrorDescription
    );
}
