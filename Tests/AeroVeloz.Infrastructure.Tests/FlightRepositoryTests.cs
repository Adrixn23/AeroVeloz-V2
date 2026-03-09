using System;
using System.Threading.Tasks;
using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Entities.Airlines;
using AeroVeloz.Domain.Entities.Flights;
using AeroVeloz.Domain.Entities.Organization.Airports;
using AeroVeloz.Infraestructure.Persistence.context;
using AeroVeloz.Infraestructure.Persistence.Repositories.Flights;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AeroVeloz.Infrastructure.Tests
{
    public class FlightRepositoryTests : IDisposable
    {
        private readonly AeroVelozContext _context;
        private readonly FlightRepository _repository;

        public FlightRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AeroVelozContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AeroVelozContext(options);
            _repository = new FlightRepository(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task CreateEntity_ShouldReturnTrue_WhenFlightIsCreated()
        {
            // Arrange
            var flight = new Flight
            {
                codeAirlines = "AA",
                OriginAirport = "JFK",
                DestinationAirport = "LAX",
                flightStateId = (byte)FlightStateEnum.Scheduled,
                ScheduledDeparture = DateTimeOffset.UtcNow
            };

            // Act
            var result = await _repository.CreateEntity(flight);

            // Assert
            Assert.True(result);
            Assert.Equal(1, await _context.Flights.CountAsync());
        }

        [Fact]
        public async Task IsOriginAirportActiveAsync_ShouldReturnTrue_WhenAirportIsActive()
        {
            // Arrange
            var airport = new Airport
            {
                codeAirportIata = "JFK",
                isActived = true
            };
            _context.Airports.Add(airport);
            await _context.SaveChangesAsync();

            // Act
            var isActive = await _repository.IsOriginAirportActiveAsync("JFK");

            // Assert
            Assert.True(isActive);
        }

        [Fact]
        public async Task IsValidDestinationAirportAsync_ShouldReturnSuccess_WhenAirportExists()
        {
            // Arrange
            var airport = new Airport
            {
                codeAirportIata = "LAX"
            };
            _context.Airports.Add(airport);
            await _context.SaveChangesAsync();

            // Act
            var validationResult = await _repository.IsValidDestinationAirportAsync("LAX");

            // Assert
            Assert.True(validationResult.IsValid);
        }

        [Fact]
        public async Task IsValidStatusTransitionAsync_ShouldReturnFailure_WhenTransitioningFromTerminalState()
        {
            // Arrange
            var flight = new Flight
            {
                flightStateId = (byte)FlightStateEnum.Cancelled
            };

            // Act
            var validationResult = await _repository.IsValidStatusTransitionAsync(flight, (byte)FlightStateEnum.Scheduled);

            // Assert
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public async Task GetFlightIdNumberAsync_ShouldReturnNextId_ForGivenAirline()
        {
            // Arrange
            var flight1 = new Flight { codeAirlines = "AA" };
            var flight2 = new Flight { codeAirlines = "AA" };
            
            _context.Flights.AddRange(flight1, flight2);
            await _context.SaveChangesAsync();

            var maxId = await _context.Flights.MaxAsync(f => f.Id);

            // Act
            var nextId = await _repository.GetFlightIdNumberAsync("AA");

            // Assert
            Assert.Equal(maxId + 1, nextId);
        }

        [Fact]
        public async Task GetByFlightAndAirlineAsync_ShouldReturnFlight_WhenExists()
        {
            // Arrange
            var airline = new Airline
            {
                codeIATA = "AA",
                nameOrganization = "American Airlines"
            };
            
            var flight = new Flight
            {
                codeAirlines = "AA",
                OriginAirport = "JFK",
                DestinationAirport = "LAX",
                flightStateId = (byte)FlightStateEnum.Scheduled,
                ScheduledDeparture = DateTimeOffset.UtcNow
            };

            _context.Airlines.Add(airline);
            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();

            // Act
            var readModel = await _repository.GetByFlightAndAirlineAsync(flight.Id, "AA");

            // Assert
            Assert.NotNull(readModel);
            Assert.Equal("AA", readModel.AirlineIataCode);
            Assert.Equal("JFK", readModel.Origin);
            Assert.Equal("LAX", readModel.Destination);
            Assert.Equal("American Airlines", readModel.nameOrganization);
        }
    }
}
