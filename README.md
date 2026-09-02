# CRN Technical Assessment – Product API

A RESTful Web API built with **ASP.NET Core Web API, Entity Framework Core, and SQL Server** for managing Products and Items.

## Features

- Product CRUD operations
- Item CRUD operations
- Product–Item relationship
- JWT authentication with refresh tokens
- Authenticated users can create, update, and delete Products/Items
- Products and Items can be viewed publicly
- FluentValidation for request validation
- Global exception handling middleware
- Layered architecture
- Swagger/OpenAPI documentation
- Pagination and `AsNoTracking()` for read operations
- Docker support

## Architecture

The project follows a simple layered architecture:

```text
API
 │
 │ Controllers
 │ Middleware
 ▼
Application
 │
 │ DTOs
 │ Services
 │ Interfaces
 ▼
Data
 │
 │ Repositories
 │ Entities
 | Validators
 │ DbContext
 ▼
SQL Server
```

### Request Flow

```text
Controller → Service → Repository → EF Core → SQL Server
```

## Project Structure

```text
API/
├── Controllers/
└── Middleware/

Application/
├── DTOs/
├── Interfaces/
├── Services/


Data/
├── Entities/
├── Repositories/
└── Validators/
└── ApplicationDbContext.cs
```

## Authentication

The API uses JWT authentication.

- `POST /api/auth/register`
- `POST /api/auth/login`

Authenticated users must provide:

```text
Authorization: Bearer <access_token>
```

The authenticated user's name is extracted from the JWT and used for `CreatedBy` and `ModifiedBy` audit fields.

## Main Endpoints

### Products

```text
GET     /api/products
GET     /api/products/{id}
POST    /api/products
PUT     /api/products/{id}
DELETE  /api/products/{id}
```

### Items

```text
GET     /api/items
GET     /api/items/{id}
GET     /api/items/product/{productId}
POST    /api/items
PUT     /api/items/{id}
DELETE  /api/items/{id}
```

`GET` endpoints are public. Create, update, and delete operations require authentication.

## Technologies

- .NET 10 / C#
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT
- FluentValidation
- Swagger / OpenAPI
- Docker

## Running the Project

Clone the repository and configure the SQL Server connection string in `appsettings.json`.

```bash
dotnet restore
dotnet ef database update
dotnet run
```

To run using Docker:

```bash
docker compose up --build
```

Swagger

Swagger/OpenAPI is available when the application is running and can be used to explore and test the API.
