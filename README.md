# 👨‍💻 UserService - Username Registry Microservice

A robust .NET 6 microservice for managing **unique usernames and account IDs**, built using **Clean Architecture**, **Domain-Driven Design (DDD)**, **CQRS with MediatR**, and supporting **multi-tenancy**.

> ✅ Built for high-scale systems handling 1M+ users with strict uniqueness requirements

---

## 🧩 Features

* ✅ Register unique usernames with associated account IDs (`Guid`)
* ✅ Enforce one username per account and vice versa
* ✅ Validate username rules (length, format, etc.)
* ✅ Replace previous username when account changes it
* ✅ Expose `GET` and `POST` REST APIs
* ✅ Designed for scalability, modularity, and testability

---

## 🏗️ Architecture Overview

This microservice follows **Clean Architecture** principles:

```
├── Domain               --> Entities, ValueObjects, Enums
├── Application          --> CQRS Commands, Queries, Interfaces, Responses
├── Infrastructure       --> EF Core DbContext, Repositories, Notifiers
├── API                  --> Controllers (REST endpoints), Middlewares
├── Tests                --> xUnit unit tests for command handlers
```

**Key Patterns Applied:**

* ✅ **CQRS** (Command Query Responsibility Segregation)
* ✅ **MediatR** for in-process messaging
* ✅ **DDD** (with aggregates & invariants)
* ✅ **Factory Method** used in aggregate root creation
* ✅ **Unit of Work** and repository pattern
* ✅ **INotifier** abstraction for domain event publishing (decoupled)

---

## 🌐 Multi-Tenancy Support

> 🔀 This microservice is **multi-tenant aware**, allowing it to support multiple clients or environments in a scalable and isolated way.

Each user record includes a `TenantId` (type `string`) that uniquely identifies the tenant/organization. This ensures:

* Separation of data between tenants
* Easy filtering and auditing per tenant
* Scalability for SaaS platforms

---

## 🔐 Authentication & External Integration

> ⚠️ This service assumes that authentication and authorization are handled externally by a dedicated **AuthService** or API Gateway. It focuses solely on **username registration logic**.

---

## 📦 Technologies

* [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* [Entity Framework Core (PostgreSQL)](https://www.npgsql.org/efcore/)
* [MediatR](https://github.com/jbogard/MediatR)
* [xUnit](https://xunit.net/)
* [Swagger / OpenAPI](https://swagger.io/)

---

## 📘 API Endpoints

### ✅ POST /api/users

Creates or updates a username for the given accountId.

```json
{
  "accountId": "2e3bcb6a-1e9a-4b58-b26c-1fb4cd3224e9",
  "username": "Erhan123",
  "tenantId": "tenant-01"
}
```

**Behavior:**

* If username exists under a different account → returns error
* If account already exists → deletes old username and replaces it

---

### 🔍 GET /api/users

Returns all registered usernames per tenant.

(Optionally extendable to include username validation endpoint)

---

## ✅ Username Validation Rules

Applied in domain aggregate:

* Required
* Length between **6 and 30 characters**
* Must be **alphanumeric**

```csharp
if (string.IsNullOrWhiteSpace(Username))
if (Username.Length < 6 || Username.Length > 30)
if (!Username.All(char.IsLetterOrDigit))
```

---

## 🧪 Unit Testing

Unit tests are written using **xUnit** and **Moq**, covering scenarios such as:

* ✅ Valid creation
* ❌ Username already exists for another account
* ✅ Username replace for same account

```csharp
[Fact]
public async Task CreateUser_Should_Return_Success_When_Valid() { /* ... */ }
```

---

## 🗃️ Database

* Using **PostgreSQL** (fast, free, production-grade)
* User table includes unique index on both `Username` and `AccountId`

```csharp
modelBuilder.Entity<Users>()
  .HasIndex(u => u.AccountId).IsUnique();
```

---

## 🚀 Getting Started

```bash
# Apply DB migrations
> dotnet ef database update

# Run the API
> dotnet run --project src/UserService.API
```

---

## 📁 Repo Structure

```
/src
  /UserService.API           --> REST endpoints
  /UserService.Application   --> CQRS logic
  /UserService.Domain        --> Core business model
  /UserService.Infrastructure --> DbContext, Repos
/tests
  /UserService.Tests         --> xUnit test project
```

---

## 📌 Final Notes

* Built to scale beyond 1M users
* Easily extendable with Redis cache, Kafka events, etc.
* All dependencies are registered via **DependencyInjection.cs**
* Make sure to provide your PostgreSQL connection string in `appsettings.json`

---

## 🛠️ Author

Developed by **Erhan Krasniqi** as part of a Full Stack (.NET/Angular) coding challenge.
