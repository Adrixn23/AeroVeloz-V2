using AeroVeloz.Application.Contracts.Flights;
using AeroVeloz.Application.DTOs.Flights;
using System.Globalization;

namespace AeroVeloz.Infraestructure.Persistence.Services
{
    public class CsvFlightParser : ICsvFlightParser
    {
        private static readonly char[] Separators = [',', ';'];

        public IReadOnlyCollection<FlightBatchItemDto> Parse(Stream csvStream, out IReadOnlyCollection<string> parseErrors)
        {
            var flights = new List<FlightBatchItemDto>();
            var errors = new List<string>();
            using var reader = new StreamReader(csvStream);

            var header = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(header))
            {
                errors.Add("El archivo CSV está vacío o no contiene encabezado");
                parseErrors = errors;
                return flights;
            }

            int row = 1;
            while (!reader.EndOfStream)
            {
                row++;
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;

                var cols = line.Split(Separators);
                if (cols.Length < 4)
                {
                    errors.Add($"Fila {row}: columnas insuficientes (mínimo 4: CodeAirlines, Origin, Destination, ScheduledDeparture)");
                    continue;
                }

                if (!DateTimeOffset.TryParse(cols[3].Trim(), CultureInfo.InvariantCulture, DateTimeStyles.None, out var departure))
                {
                    errors.Add($"Fila {row}: formato de fecha inválido '{cols[3].Trim()}'");
                    continue;
                }

                flights.Add(new FlightBatchItemDto(
                    0, // id por el momento
                    cols[0].Trim(),
                    cols[1].Trim(),
                    cols[2].Trim(),
                    departure,
                    cols.Length > 4 ? cols[4].Trim() : null,
                    cols.Length > 5 ? cols[5].Trim() : null
                ));
            }

            parseErrors = errors;
            return flights;
        }
    }
}
