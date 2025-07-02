# UserRegistryService (.NET 8 | Clean Architecture | CQRS | PostgreSQL)

This microservice is designed to **register and validate usernames and account IDs** in a **multi-tenant** environment. It ensures:

* ✅ Each username is unique
* ✅ One username per account (System.Guid)
* ✅ Username constraints (6–30 chars, alphanumeric only)
* ✅ Full support for validation, creation, deletion, and testing

---

## 🧱 Architecture Overview

* **.NET 8** with **ASP.NET Core Web API**
* **Clean Architecture** with layered separation
* **Domain-Driven Design (DDD)**
* **CQRS** pattern with **MediatR**
* **Factory Method** for consistent domain creation
* **xUnit + Moq** for unit testing
* **PostgreSQL** for persistence
* **Entity Framework Core** as ORM
* **INotifier** interface for future domain event notifications
* **TenantId** support for **multi-tenant architecture**

---

## 📆 Technologies

| Layer          | Stack                                         |
| -------------- | --------------------------------------------- |
| API            | ASP.NET Core 8                                |
| Application    | CQRS, MediatR                                 |
| Domain         | DDD, AggregateRoot                            |
| Infrastructure | PostgreSQL, EF Core, UnitOfWork, Repositories |
| Tests          | xUnit, Moq                                    |
| Eventing       | INotifier abstraction                         |

--- 
 
## 📁 Project Structure

```
UserService/
├── API/
│   └── Controllers/
├── Application/
│   ├── Features/
│   └── Responses/
├── Domain/
│   └── Aggregates/
├── Infrastructure/
│   ├── Repositories/
│   └── Database/
├── SharedKernel/
├── Tests/
│   └── CreateUserTest.cs
```





## 🌐 Multi-Tenancy Support

> 🔀 This microservice is **multi-tenant aware**, allowing it to support multiple clients or environments in a scalable and isolated way.

Each user record includes a `TenantId` (type `string`) that uniquely identifies the tenant/organization. This ensures:

* Separation of data between tenants
* Easy filtering and auditing per tenant
* Scalability for SaaS platforms

---

## 🥪 Testing

Unit tests are implemented using `xUnit` and `Moq`. These cover:

* ✅ Successful user creation
* ✅ Username conflict validation
* ✅ Replacing old username when `AccountId` exists

---

## 🧰 EF Core & PostgreSQL Setup

Make sure you have the PostgreSQL connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DbConnection": "Host=localhost;Port=5432;Database=WitsDB;Username=admin;Password=admin"
}
```

Then run:

```bash
# Create migration
dotnet ef migrations add InitialCreate --project UserService.Infrastructure --startup-project UserService.API

# Apply migration to DB
dotnet ef database update --project UserService.Infrastructure --startup-project UserService.API
```

> ℹ️ Make sure the EF CLI is installed:

```bash
dotnet tool install --global dotnet-ef
```

---

## ✨ How to Run

```bash
# In terminal
dotnet build
dotnet run --project UserService.API
```

The Swagger UI will be available at:
`https://localhost:5001/swagger`

---



---
 

## 📃 Example API Usage

```http
POST /api/users
Content-Type: application/json

{
  "accountId": "fdc14e26-a29f-420b-930e-213c1be26f3e",
  "username": "PlayerOne123",
  "tenantId": "game-server-01"
}
```

Response:

```json
{
  "success": true,
  "message": "User created successfully.",
  "result": 1
}
```

## 🔐 Authentication & External Integration

> ⚠️ This service assumes that authentication and authorization are handled externally by a dedicated **AuthService** or API Gateway. It focuses solely on **username registration logic**.

---

## 📓 Author

Made with ❤️ by Erhan Krasniqi


 
