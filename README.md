# The Springfield Dungeon 🏰

A personal REST API project built with **.NET 10** and **C# 14**, used as a playground for exploring clean architecture, CQRS, the Result pattern, and modern ASP.NET Core features.

---

## 📁 Solution Structure

```
TheSpringfieldDungeon/
├── TSD.Host/             # Entry point & composition root (ASP.NET Core host)
├── TSD.Api/              # Controllers, DTOs, AutoMapper profiles
├── TSD.Domain/           # Entities, Commands, Queries, Handlers, Repository interfaces
└── TSD.Infrastructure/   # EF Core DbContext, Repository implementations
```

### Project dependency graph

```
TSD.Host
├── TSD.Api
│   └── TSD.Domain
└── TSD.Infrastructure
    └── TSD.Domain
```

`TSD.Domain` has no project references — it is the dependency-free core.

---

## 🛠️ Tech Stack

| Concern              | Library / Technology                                |
|----------------------|-----------------------------------------------------|
| Framework            | ASP.NET Core (.NET 10)                              |
| Language             | C# 14                                               |
| Database             | SQLite via Entity Framework Core 10                 |
| CQRS / Mediator      | MediatR 14                                          |
| Object Mapping       | AutoMapper 16                                       |
| API Documentation    | Swashbuckle (Swagger UI) + Microsoft.AspNetCore.OpenApi |
| Domain Validation    | CSharpFunctionalExtensions — `Result<T>` pattern    |
| Error Handling       | RFC 7807 Problem Details + `IExceptionHandler`      |

---

## 🏗️ Architecture

The solution follows a **Clean Architecture** approach with a strict **CQRS** split:

- **Commands** mutate state. They are dispatched via MediatR, validated in the handler, and persisted through a repository interface.
- **Queries** are read-only. Handlers retrieve data via repositories and map to response objects using AutoMapper.
- **Entities** are constructed through a static `Create()` factory method that returns `Result<T>`, enforcing invariants without throwing exceptions.
- **Repository interfaces** live in `TSD.Domain`; implementations live in `TSD.Infrastructure`, keeping the domain free of infrastructure concerns.
- **AutoMapper profiles** exist at two levels: `TSD.Api` maps between DTOs and domain requests/responses; `TSD.Domain` maps between entities and query response objects.

### Request flow (example: create customer)

```
POST /api/customers
    │
    ▼
CustomersController.Create()
    │  maps CustomerDto → AddCustomerCommandRequest (AutoMapper)
    ▼
IMediator.Send(AddCustomerCommandRequest)
    │
    ▼
AddCustomerCommandHandler.Handle()
    ├── ICustomerRepository.IsExternalIdUnique()  — cross-entity DB check
    ├── Customer.Create(request)                  — domain invariant validation (Result<T>)
    └── ICustomerRepository.AddAsync() + SaveChangesAsync()
    │
    ▼
201 Created  { id: "..." }
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### Run

```bash
cd TSD.Host
dotnet run
```

The SQLite database is created and all pending EF Core migrations are applied automatically on startup. No manual `dotnet ef database update` is required during development.

Swagger UI is available at:

```
https://localhost:{port}/swagger
```

---

## 📡 API Endpoints

### Customers

| Method | Route                 | Description           | Success | Error            |
|--------|-----------------------|-----------------------|---------|------------------|
| POST   | `/api/customers`      | Create a new customer | 201     | 400 (validation) |
| GET    | `/api/customers/{id}` | Get a customer by ID  | 200     | 404              |

### Customer DTO

```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "fullName": "Homer Simpson",
  "externalId": 42
}
```

### Validation rules

| Field        | Rule                              |
|--------------|-----------------------------------|
| `fullName`   | Required, cannot be empty         |
| `externalId` | Required, must be a positive integer and unique across all customers |

---

## ⚙️ Key Design Decisions

### Result pattern over exceptions
Domain entity creation uses `CSharpFunctionalExtensions.Result<T>` to surface validation failures as values rather than exceptions, keeping the happy path clean and errors explicit.

### Abstract command base
`SaveCustomerCommandRequest` is a shared abstract base for `AddCustomerCommandRequest` and `UpdateCustomerCommandRequest`, avoiding duplication of shared properties (`FullName`, `ExternalId`) across command types.

### Assembly scanning via Module classes
Each project exposes a static `Module` class with `GetAssembly()`, used in `Program.cs` to register controllers, MediatR handlers, and AutoMapper profiles without hard-coding assembly names.

### Global exception handling
`GlobalExceptionHandler` implements `IExceptionHandler` and converts unhandled exceptions to RFC 7807 Problem Details responses, with structured logging on every error.
