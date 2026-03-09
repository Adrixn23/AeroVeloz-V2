# AeroVeloz Project Overview

AeroVeloz is a flight information system designed for real-time consultation of flights from various airlines. It provides updated information to users and allows for flight management and auditing within an airport context.

## Technical Stack
- **Framework:** .NET 9.0
- **Architecture:** Clean Architecture / N-Layered
- **Database:** SQL Server with Entity Framework Core
- **Messaging/Patterns:** MediatR for CQRS/decoupling
- **UI:** 
  - Web: ASP.NET Core Razor Pages
  - Desktop: WPF (Windows Presentation Foundation)
  - API: ASP.NET Core Web API (OpenAPI/Swagger enabled)

## Project Structure
- **Api/AeroVeloz.Api:** The entry point for external integrations and frontend consumers.
- **Core/AeroVeloz.Domain:** Contains the core business logic, including entities (Flights, Airlines, Users, etc.), domain services, and interfaces.
- **Core/AeroVeloz.Application:** Implements the application use cases, DTOs, and service logic. Uses MediatR for orchestration.
- **Infraestructure/AeroVeloz.Infraestructure:** Handles data persistence using Entity Framework Core and implements repository interfaces defined in the Core.
- **Presentacion/AeroVeloz.Web:** A web-based user interface using Razor Pages.
- **Presentacion/AeroVeloz.Desktop:** A desktop application for management and consultation.
- **Transversal/AeroVeloz.Transversal:** Shared utilities and cross-cutting concerns (logging, mapping, etc.).
- **Aeroveloz.IOC:** An ASP.NET Core MVC project, likely intended for centralizing service registration or acting as a management portal.

## Building and Running
### Prerequisites
- .NET 9.0 SDK
- SQL Server

### Key Commands
- **Restore Dependencies:** `dotnet restore`
- **Build Solution:** `dotnet build`
- **Run API:** `dotnet run --project Api/AeroVeloz.Api/AeroVeloz.Api.csproj`
- **Run Web UI:** `dotnet run --project Presentacion/AeroVeloz.Web/AeroVeloz.Web.csproj`
- **Database Migrations:** `dotnet ef database update --project Infraestructure/AeroVeloz.Infraestructure/AeroVeloz.Infraestructure.csproj --startup-project Api/AeroVeloz.Api/AeroVeloz.Api.csproj`

## Development Conventions
- **Entity Base:** Most domain entities inherit from `BEntity<short>`.
- **Database Context:** `AeroVelozContext` in the Infrastructure layer manages the DB sets and configurations.
- **API Documentation:** Swagger/OpenAPI is available in development mode for the API project.
- **Spanish Documentation:** The primary `README.md` and some business terminology are in Spanish.
