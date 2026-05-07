# AeroVeloz - System Architecture Guide

<div align="center">

**Comprehensive Technical Architecture Documentation**

*Understanding the design, patterns, and structure of AeroVeloz*

</div>

---

## 📋 Table of Contents

1. [Architecture Overview](#architecture-overview)
2. [Design Principles](#design-principles)
3. [Layered Architecture](#layered-architecture)
4. [Design Patterns](#design-patterns)
5. [Detailed Layer Breakdown](#detailed-layer-breakdown)
6. [Data Flow](#data-flow)
7. [Domain Entities](#domain-entities)
8. [Event-Driven Architecture](#event-driven-architecture)
9. [Dependency Injection](#dependency-injection)
10. [Security Architecture](#security-architecture)

---

## 🎯 Architecture Overview

AeroVeloz is built using **Clean Architecture** principles with a strong emphasis on domain-driven design and event-driven patterns. The system is organized into distinct layers that promote maintainability, testability, and scalability.

```
┌────────────────────────────────────────────────────────┐
│         Presentation Layer (WPF Desktop UI)            │
│  ├─ Views (XAML)                                       │
│  ├─ ViewModels (MVVM)                                  │
│  └─ Services (UI-specific logic)                       │
└────────────────────────────────────────────────────────┘
                          │
                          │ HTTP/WPF Events
                          ↓
┌────────────────────────────────────────────────────────┐
│      Application Layer (Use Cases & Orchestration)     │
│  ├─ Services (IAuthenticationService, IUserService)   │
│  ├─ DTOs (Data Transfer Objects)                      │
│  ├─ Repositories (Abstraction)                        │
│  ├─ Event Handlers (AirportEventService, etc.)        │
│  └─ Contracts (Interface definitions)                 │
└────────────────────────────────────────────────────────┘
                          │
                          │ Business Operations
                          ↓
┌────────────────────────────────────────────────────────┐
│    Domain Layer (Pure Business Logic & Entities)       │
│  ├─ Entities (User, Airport, Flight, etc.)            │
│  ├─ Domain Events (UserCreatedEvent, etc.)            │
│  ├─ Domain Services (IDomainServiceUser, etc.)        │
│  ├─ Validators (Business rules validation)            │
│  └─ Common (Exceptions, Value Objects)                │
└────────────────────────────────────────────────────────┘
                          │
                          │ Data Persistence
                          ↓
┌────────────────────────────────────────────────────────┐
│   Infrastructure Layer (Data Access & External SVCs)  │
│  ├─ Database (Entity Framework Core)                  │
│  ├─ Repository Implementations                        │
│  ├─ External Service Integrations                     │
│  ├─ Notification Providers                            │
│  └─ Configuration                                     │
└────────────────────────────────────────────────────────┘
                          │
                          │
                          ↓
            ┌─────────────────────────┐
            │  SQL Server Database    │
            │  External APIs & Svcs   │
            │  File Systems           │
            └─────────────────────────┘
```

---

## 🏗️ Design Principles

### 1. **Clean Architecture (Onion Architecture)**
- **Independence**: Core business logic is independent of frameworks, databases, and UI
- **Dependency Rule**: Dependencies point inward; outer layers depend on inner layers
- **Testability**: Each layer can be tested independently
- **Flexibility**: Easy to change databases, UI, or frameworks

### 2. **Domain-Driven Design (DDD)**
- **Bounded Contexts**: Clear domain boundaries and responsibilities
- **Entities**: Objects with unique identities (User, Airport, Flight)
- **Value Objects**: Immutable objects without identity (RolePermission, OperationalChangeType)
- **Domain Events**: Represent significant state changes (UserCreatedDomainEvent)
- **Domain Services**: Business logic that doesn't fit in entities (IDomainServiceUser)

### 3. **SOLID Principles**

| Principle | Implementation |
|-----------|----------------|
| **S** - Single Responsibility | Each service/class has one reason to change |
| **O** - Open/Closed | Open for extension, closed for modification |
| **L** - Liskov Substitution | Implementations can replace abstractions |
| **I** - Interface Segregation | Small, focused interfaces (IUserService, IAirportService) |
| **D** - Dependency Inversion | Depend on abstractions, not concrete implementations |

### 4. **CQRS (Command Query Responsibility Segregation)**
- **Separate Read & Write**: Different models for reading and writing
- **MediatR Implementation**: Centralized command/query bus
- **Scalability**: Optimize read and write paths independently

### 5. **Event-Driven Architecture**
- **Domain Events**: Publish domain events for important state changes
- **Event Handlers**: Services react to domain events asynchronously
- **Decoupling**: Publishers and subscribers are loosely coupled
- **Auditability**: Complete history of all system state changes

---

## 📊 Layered Architecture

### Layer Characteristics

| Layer | Purpose | Dependencies | Key Artifacts |
|-------|---------|--------------|----------------|
| **Presentation** | User interface & interaction | Application, WPF | Views, ViewModels, XAML |
| **Application** | Business orchestration | Domain | Services, DTOs, Event Handlers |
| **Domain** | Pure business logic | None (independent) | Entities, Events, Validators |
| **Infrastructure** | Data access & external services | All layers | DbContext, Repositories, Providers |

### Architecture Invariants

1. **Never bypass layers** - Always go through the proper service layer
2. **Domain stays pure** - No infrastructure or framework dependencies in Domain
3. **DTOs at boundaries** - Transfer objects between layers
4. **Repositories abstract data** - Infrastructure details hidden from Application layer
5. **Events drive changes** - State changes trigger domain events

---

## 🔄 Design Patterns

### 1. Repository Pattern
Abstracts data access, allowing multiple implementations:

```
IUserRepository (Application)
        ↑
        └─ UserRepository (Infrastructure)
```

**Benefits:**
- Testability with mock repositories
- Database independence
- Centralized data access logic

### 2. Service Locator Pattern (Dependency Injection)
All dependencies injected at composition root:

```
IServiceProvider (IOC Layer)
├─ IAuthenticationService
├─ IUserService
├─ IAirportService
└─ ...more services
```

**Benefits:**
- Loose coupling between services
- Easy to mock for testing
- Centralized configuration

### 3. CQRS Pattern with MediatR
Separates read (Query) and write (Command) operations:

```
UserLoginDto → Command → IRequestHandler<,> → Result
FlightListDto ← Query ← IRequestHandler<,> ← QueryObject
```

**Benefits:**
- Optimized read/write operations
- Clear command/query distinction
- Easier scaling of read-heavy operations

### 4. Strategy Pattern
Multiple implementations for different scenarios:

```
INotificationChannel
├─ EmailNotificationChannel
├─ SmsNotificationChannel
└─ PushNotificationChannel
```

### 5. Observer Pattern
Event handling for domain changes:

```
Domain Event (UserCreated)
        ↓
    Event Handler
├─ AuditEventService
├─ NotificationEventService
└─ LoggingEventService
```

### 6. Validator Pattern
Encapsulates business rule validation:

```
IUserValidator → Validates user creation/update rules
IOperationalChangeValidator → Validates operational changes
IAirportValidator → Validates airport-specific rules
```

### 7. Factory Pattern
Creating complex objects with consistency:

```
OperationResult
├─ Success()
├─ Failure()
└─ ValidationError()
```

---

## 🔍 Detailed Layer Breakdown

### Domain Layer (`Core/AeroVeloz.Domain`)

**Responsibility:** Pure business logic independent of frameworks.

**Key Components:**

#### Entities
Aggregate roots representing core business concepts:
- **User** - System user with roles and permissions
- **Airport** - Airport organization with settings
- **Airline** - Airline company
- **Flight** - Individual flight information
- **Organization** - Parent organization container
- **OperationChange** - Operational event/change
- **Audit** - Immutable audit record

```csharp
public class User : BEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public Roles Role { get; set; }
    public bool IsActive { get; set; }
    // Business methods
    public void Lock() { /* logic */ }
    public void ChangeRole(Roles newRole) { /* logic */ }
}
```

#### Domain Events
Represent significant state changes:
- `UserCreatedDomainEvent`
- `UserUpdatedDomainEvent`
- `AirportRegisteredDomainEvent`
- `OperationalChangeRegisteredDomainEvent`
- `AuditRecordCreatedDomainEvent`

```csharp
public class UserCreatedDomainEvent : IDomainEvent
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

#### Domain Services
Business logic that spans multiple entities:

| Service | Purpose |
|---------|---------|
| `IDomainServiceUser` | User-specific business rules |
| `IDomainServiceAirport` | Airport operations logic |
| `IDomainServiceOrganization` | Organization-level rules |
| `IDomainServiceOperationalChange` | Operational change validation |

#### Validators
Encapsulate business rule validation:
- `UserValidator` - User creation/update rules
- `AirportValidator` - Airport configuration rules
- `OperationalChangeValidator` - Change validation rules

#### Code Errors & Exceptions
Standardized error definitions:
- `UserErrors.cs` - User-related errors
- `AiportErrors.cs` - Airport-related errors
- `OperationalChangeErrors.cs` - Operational errors
- `AuthenticationErrors.cs` - Auth-related errors

### Application Layer (`Core/AeroVeloz.Application`)

**Responsibility:** Orchestration of use cases and application logic.

**Key Components:**

#### Services
High-level business operations:

| Service | Interface | Purpose |
|---------|-----------|---------|
| `AuthenticationService` | `IAuthenticationService` | User login and token generation |
| `UserService` | `IUserService` | User CRUD operations |
| `AirportService` | `IAirportService` | Airport management |
| `AirportConnectionService` | `IAirportConnectionService` | Airline-Airport connections |
| `OperationalService` | `IOperationalService` | Flight operations |
| `AuditService` | `IAuditService` | Audit log management |
| `FlightService` | `IFlightService` | Flight information |
| `StatsService` | `IStatsService` | System statistics |

#### Data Transfer Objects (DTOs)
Objects for data transfer between layers:

```
User Entity → UserDetailModel → UserUpdateDto → UI Layer
```

**DTO Categories:**
- **Save DTOs** (UserSaveDto) - Create operations
- **Update DTOs** (UserUpdateDto) - Modify operations
- **Detail DTOs** (UserDetailModel) - Rich detail objects
- **Remove DTOs** (UserRemoveDto) - Delete operations
- **List DTOs** (FlightListDto) - Collection items

#### Repositories (Interfaces)
Abstractions for data access:

```csharp
public interface IUserRepository : IBRepository<User>
{
    Task<User> GetByUsernameAsync(string username);
    Task<List<User>> GetByRoleAsync(Roles role);
    Task<bool> UsernameExistsAsync(string username);
}
```

**Repository Abstractions:**
- `IUserRepository` - User data access
- `IAirportRepository` - Airport data access
- `IAirportConnectionAirline` - Connection data access
- `IFlightRepository` - Flight data access
- `IOperationalRepository` - Operation data access
- `IAuditRepository` - Audit log access
- `INotificationDispatcher` - Notification sending

#### Event Services
Handles domain event reactions:

```csharp
public class UserEventService : INotificationHandler<UserCreatedDomainEvent>
{
    public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        // React to user creation
        // Send notifications, update stats, etc.
    }
}
```

**Event Services:**
- `UserEventService` - User-related events
- `AirportEventService` - Airport-related events
- `OperationalEventService` - Operational changes
- `AuditEventService` - Audit logging

#### Operation Result Pattern
Standardized result handling:

```csharp
public class OperationResult<T>
{
    public bool IsSuccess { get; set; }
    public T Data { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; }
}
```

### Infrastructure Layer (`Infraestructure/AeroVeloz.Infraestructure`)

**Responsibility:** Data persistence, external integrations, and concrete implementations.

**Key Components:**

#### Entity Framework Core DbContext
Database schema and mappings:
- Entity mappings
- Relationship configurations
- Query optimization
- Database migrations

#### Repository Implementations
Concrete implementations of Application layer interfaces:

```csharp
public class UserRepository : BRepository<User>, IUserRepository
{
    public async Task<User> GetByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
}
```

#### Notification Providers
Multi-channel notification system:

```
INotificationDispatcher
├─ EmailProvider
├─ SmsProvider
└─ PushNotificationProvider
```

**Provider Implementation:**
- Interface: `INotificationChannel`
- Implementation: Specific provider (EmailChannel, SmsChannel, etc.)

### Presentation Layer (`Presentacion/AeroVeloz.Desktop`)

**Responsibility:** User interface and user interaction.

**Architecture: MVVM (Model-View-ViewModel)**

#### Views (XAML)
User interface components:
- `SuperAdminMainView.xaml` - Super Admin interface
- `AdminListView.xaml` - Admin list management
- `AirportAdminDashboardView.xaml` - Airport dashboard
- `UserListView.xaml` - User list management
- `OperationsListView.xaml` - Operations list
- `ConnectionListView.xaml` - Connection management

#### ViewModels
MVVM logic layer using Community Toolkit:

```csharp
public class UserListViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<UserDetailModel> users;

    [RelayCommand]
    private async Task LoadUsers()
    {
        var result = await _userService.GetAllAsync();
        Users = new ObservableCollection<UserDetailModel>(result.Data);
    }
}
```

**Features:**
- Observable properties for data binding
- Relay commands for user actions
- Async/await for non-blocking operations

#### Services
UI-specific services:
- Navigation
- Dialog management
- Message boxes
- Theme management

### API Layer (`Api/AeroVelozDesktop.Api`)

**Responsibility:** REST API endpoints for desktop integration.

**Structure:**
- Controllers for each domain module
- Request/Response mapping
- Authentication middleware
- Error handling

### IOC Layer (`IOC/AeroVeloz.IOC`)

**Responsibility:** Dependency injection configuration.

```csharp
public static IServiceCollection AddApplicationServices(this IServiceCollection services)
{
    // Domain Services
    services.AddScoped<IDomainServiceUser, DomainServiceUser>();
    services.AddScoped<IDomainServiceAirport, DomainServiceAirport>();
    
    // Application Services
    services.AddScoped<IAuthenticationService, AuthenticationService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IAirportService, AirportService>();
    
    // Infrastructure
    services.AddScoped(typeof(IBRepository<>), typeof(BRepository<>));
    services.AddScoped<IUserRepository, UserRepository>();
    
    return services;
}
```

---

## 🔀 Data Flow

### User Authentication Flow

```
1. Login View (User Input)
   ↓
2. LoginViewModel.LoginCommand
   ↓
3. AuthenticationService.AuthenticateAsync(credentials)
   ↓
4. UserRepository.GetByUsernameAsync() [Infrastructure]
   ↓
5. User Entity Retrieved from Database
   ↓
6. Password verification (Business Logic)
   ↓
7. JwtProvider.GenerateToken()
   ↓
8. UserLoginResultDto returned
   ↓
9. Store token in secure location
   ↓
10. Navigate to Dashboard
    ↓
11. Publish UserLoginDomainEvent
    ↓
12. Event Handlers React (Logging, Notifications)
```

### Flight Data Flow (Read)

```
1. Dashboard View initialization
   ↓
2. FlightListViewModel.LoadFlights()
   ↓
3. FlightService.GetAllAsync()
   ↓
4. FlightRepository.GetAllAsync() [Infrastructure]
   ↓
5. EF Core Query Execution
   ↓
6. Database Query → SQL Server
   ↓
7. Results mapped to FlightListDto
   ↓
8. Return to ViewModel
   ↓
9. Bind to ObservableCollection<FlightListDto>
   ↓
10. WPF Data Binding Updates UI
```

### Operational Change Flow (Write)

```
1. OperationDetailView - Change Status Action
   ↓
2. OperationDetailViewModel.UpdateOperation()
   ↓
3. OperationalService.UpdateAsync(OperationalChangeUpdateDto)
   ↓
4. Domain Validator Checks Rules
   ↓
5. OperationalChange Entity Updated
   ↓
6. Publish OperationalChangeRegisteredDomainEvent
   ↓
7. Save to Database (EF Core)
   ↓
8. Event Handlers Execute:
   ├─ Create Audit Record
   ├─ Send Notifications
   ├─ Update Statistics
   └─ Log Changes
   ↓
9. Return OperationResult<T>
   ↓
10. UI Updated with Result
```

---

## 🗂️ Domain Entities

### Entity Relationships

```
Organization
├─ Airport (1:N)
│  ├─ ConectionsAirlineAirport (1:N)
│  │  └─ Airline (N:1)
│  └─ Flight (1:N)
│     ├─ FlightHistory (1:N)
│     └─ OperationChange (1:N)
├─ User (1:N)
│  ├─ Roles (N:1)
│  │  └─ Permissions (N:N)
│  └─ Audit (1:N)
└─ Subscription (1:N)
   └─ ChannelSubscriptionNotification (1:N)
```

### Core Entities

#### User Aggregate
- **User** (Root)
  - Username, Email
  - Role assignment
  - Account status (Active, Locked)
  - Created/Updated tracking

#### Airport Aggregate
- **Airport** (Root)
  - Airport name and code
  - Organization reference
  - Status (Active, Suspended)
  - Airline connections

- **ConectionsAirlineAirport**
  - Defines valid Airline-Airport pairs
  - Configuration per connection
  - Activation status

#### Flight Aggregate
- **Flight** (Root)
  - Flight number, status
  - Departure/Arrival info
  - Associated airline
  - Flight history tracking

- **FlightHistory**
  - Status change records
  - Timestamp tracking
  - Change reason

#### Operation Aggregate
- **OperationChange** (Root)
  - Change type (Gate change, Status update)
  - Flight reference
  - Change details
  - Timestamp and user tracking

#### Audit Aggregate
- **Audit** (Root)
  - Immutable record of changes
  - User action tracking
  - Timestamp and details
  - Cannot be deleted

---

## 📡 Event-Driven Architecture

### Domain Events

Events represent important state changes in the domain:

```csharp
public interface IDomainEvent
{
    DateTime OccurredOn { get; }
    Guid AggregateId { get; }
}
```

### Event Publishing Flow

```
Entity State Change
    ↓
Publish Domain Event
    ↓
Domain Event Dispatcher
    ↓
┌───────────────────────────┐
│   Event Handlers          │
├───────────────────────────┤
│ - Audit Event Handler     │
│ - Notification Handler    │
│ - Logging Handler         │
│ - Stats Update Handler    │
└───────────────────────────┘
    ↓
Side Effects Executed
```

### Event Handler Example

```csharp
public class AuditEventService : 
    INotificationHandler<UserCreatedDomainEvent>,
    INotificationHandler<AirportRegisteredDomainEvent>
{
    public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var audit = new Audit
        {
            UserId = notification.UserId,
            Action = "User Created",
            Details = notification.Username,
            Timestamp = DateTime.UtcNow
        };
        
        await _auditRepository.AddAsync(audit);
    }
}
```

### Supported Domain Events

| Event | Trigger | Handler |
|-------|---------|---------|
| `UserCreatedDomainEvent` | User creation | Audit, Notification |
| `UserUpdatedDomainEvent` | User modification | Audit |
| `AirportRegisteredDomainEvent` | New airport | Audit, Notification |
| `AirportConnectionCreatedDomainEvent` | New connection | Audit |
| `OperationalChangeRegisteredDomainEvent` | Flight status change | Audit, Notification |
| `AuditRecordCreatedDomainEvent` | Audit creation | Logging |

---

## 💉 Dependency Injection

### Composition Root

Configured in `Startup.cs` or `Program.cs`:

```csharp
// Domain Services
builder.Services.AddScoped<IDomainServiceUser>();
builder.Services.AddScoped<IDomainServiceAirport>();
builder.Services.AddScoped<IDomainServiceOperationalChange>();

// Application Services
builder.Services.AddScoped<IAuthenticationService>();
builder.Services.AddScoped<IUserService>();
builder.Services.AddScoped<IAirportService>();
builder.Services.AddScoped<IOperationalService>();

// Infrastructure
builder.Services.AddDbContext<AeroVelozDbContext>();
builder.Services.AddScoped(typeof(IBRepository<>), typeof(BRepository<>));

// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Notification
builder.Services.AddScoped<INotificationDispatcher>();
```

### Service Lifetime

| Lifetime | Usage | Example |
|----------|-------|---------|
| **Transient** | Lightweight, stateless | Validators |
| **Scoped** | Per request/operation | Services, Repositories |
| **Singleton** | Application-wide | Configuration, Logging |

---

## 🔐 Security Architecture

### Authentication

**JWT-based token authentication:**

```
1. User submits credentials
   ↓
2. Authenticate against User entity
   ↓
3. Generate JWT token (includes UserId, Role, Claims)
   ↓
4. Return token to client
   ↓
5. Client includes token in Authorization header
   ↓
6. Server validates token signature
   ↓
7. Extract claims for authorization
```

**JWT Structure:**
```json
{
  "header": {
    "alg": "HS256",
    "typ": "JWT"
  },
  "payload": {
    "sub": "user-id",
    "role": "AirportAdmin",
    "exp": 1234567890
  },
  "signature": "..."
}
```

### Authorization

**Role-Based Access Control (RBAC):**

```
User → Role → Permissions
       ├─ SuperAdmin → All Permissions
       ├─ AirportAdmin → Airport-specific Permissions
       └─ SystemUser → Read-only Permissions
```

### Audit & Compliance

**Immutable audit trail:**

```csharp
public class Audit : BEntity
{
    public Guid UserId { get; set; }
    public string Action { get; set; }
    public string Details { get; set; }
    public DateTime Timestamp { get; set; }
    
    // Audit records cannot be deleted
    // Only created or queried
}
```

### Data Protection

- **Connection String**: Stored in User Secrets
- **Sensitive Data**: JWT secrets in User Secrets
- **Database**: TrustServerCertificate for development
- **Transmission**: HTTPS in production

---

## 🚀 Scalability Considerations

### Horizontal Scaling
- **Stateless Services**: All services can be stateless
- **Database Replication**: SQL Server replication
- **API Load Balancing**: Multiple API instances

### Vertical Scaling
- **Async Operations**: Async/await throughout
- **Caching**: Implement caching for frequent queries
- **Database Indexing**: Optimize database indexes

### Performance Optimization
1. **Query Optimization**: Use Select() for partial projections
2. **Pagination**: Implement for large result sets
3. **Caching**: Cache immutable data (Airports, Airlines)
4. **Async all the way**: No blocking operations

---

## 📚 Technology Stack Reference

| Component | Technology | Version |
|-----------|-----------|---------|
| Framework | .NET | 9.0 |
| Language | C# | 12.0 |
| ORM | Entity Framework Core | 9.0 |
| UI Framework | WPF | Windows 10.0.19041.0 |
| MVVM Toolkit | Community Toolkit | 8.4.0 |
| CQRS Bus | MediatR | 13.1.0 |
| Authentication | JWT | Custom |
| Database | SQL Server | 2019+ |
| Real-time | SignalR | 10.0.5 |

---

## 🔗 Related Documentation

- [Environment Setup & Installation Guide](./ENVIRONMENT_SETUP_AND_INSTALLATION_GUIDE.md)
- [README - Quick Reference](../README.md)

---

<div align="center">

**AeroVeloz - Enterprise Flight Management Architecture**

*Built on Clean Architecture principles and Domain-Driven Design*

</div>
