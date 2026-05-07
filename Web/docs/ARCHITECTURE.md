# Architecture & Design Principles

## Overview
**AeroVeloz** is built strictly following **Clean Architecture (Onion Architecture)** principles. The primary goal of this architecture is to separate the core business rules from external frameworks, interfaces, and databases. 

By keeping the core independent, the system achieves:
- **High Testability:** Core logic can be unit-tested without needing a real database or HTTP server.
- **Framework Independence:** The UI or database engine can be swapped without altering business rules.
- **Maintainability:** Clear boundaries prevent "spaghetti code."

---

## 🏗️ Layer Breakdown

### 1. Domain Layer (`Core\AeroVeloz.Domain`)
This is the heart of the system. It has **no external dependencies**.
- **Entities:** Core business objects (`Flight`, `Airline`, `Airport`, `User`, `Organizations`).
- **Enums & Value Objects:** Strongly typed constraints (`FlightStateEnum`, etc.).
- **Events:** Domain Events that occur within the system (e.g., `FlightStatusChangedEvent`).
- **Interfaces (Contracts):** Definitions of what the repositories must implement.
- **Validation:** Built-in validation rules using FluentValidation or custom logical validations.

### 2. Application Layer (`Core\AeroVeloz.Application`)
This layer defines the **Use Cases** of the system. It depends *only* on the Domain layer.
- **DTOs:** Data Transfer Objects to transport data without exposing core Entities.
- **Services/Handlers:** Implementations of business operations (`FlightService`, `AuthService`).
- **CQRS:** Command Query Responsibility Segregation approach using `MediatR` to separate write operations from read operations.
- **OperationResult Pattern:** Standardized return types containing success flags, messages, data payloads, and HTTP-agnostic error codes.

### 3. Infrastructure Layer (`Infraestructure\AeroVeloz.Infraestructure`)
This layer implements all the external concerns.
- **Persistence:** Entity Framework Core implementation, `DbContext`, and Repository concrete classes (`AirlineRepository`, `FlightRepository`).
- **Migrations:** SQL schema definitions.
- **External Integrations:** APIs, Email Senders (SMTP), Push Notification services.

### 4. Transversal Layer (`Transversal\AeroVeloz.Transversal`)
A cross-cutting concerns layer accessible by all other layers.
- **Logging & Monitoring:** Standardized error logging structures.
- **Security:** Global security contracts.

### 5. Inversion of Control Layer (`IOC\AeroVeloz.IOC`)
The centralized dependency injection container.
- **Dependencies Registration:** Consolidates all `IServiceCollection` configurations for Application, Infrastructure, and Domain services, maintaining a clean `Program.cs`.

### 6. Presentation Layer / Web API (`Api\AeroVeloz.Api` & `Presentacion\AeroVeloz.Web`)
The entry points to the system.
- **API Controllers:** Minimal, thin controllers that map HTTP requests to Application Services and HTTP responses.
- **Razor Pages (Web):** The frontend application consuming the REST API. Contains strictly UI logic and API client services (`FlightApiService`).

---

## 🔒 Security & Roles (RBAC)

The system uses **JWT (JSON Web Tokens)** for stateless authentication. Security is managed through robust Role-Based Access Control:

1.  **SYSTEMADMIN:** Global access, can manage organizations, edit all users, view global audits.
2.  **AIRLINEADMIN:** Can manage flights specific to their airline, upload batch itineraries.
3.  **AIRPORTADMIN:** Can manage physical operations for a specific airport.
4.  **OPERATIONAIRPORT:** Staff members updating boarding gates and real-time statuses on the tarmac.

Each entity mutation is securely audited using `AuditService` to trace `UserId`, `OrganizationId`, timestamps, and exact `JSON` value changes.