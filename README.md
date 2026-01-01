# AlzaProductApi

> **Simple REST API for managing products**, implemented as a test assignment.

The solution demonstrates:

- ✅ clean separation of layers (**Web / Core / Infrastructure**)
- ✅ **API versioning** (v1, v2)
- ✅ **paginated endpoints**
- ✅ **Entity Framework Core** with SQL Server
- ✅ **Swagger / OpenAPI** documentation

---

## 📦 Requirements

- **.NET 8 SDK**
- **SQL Server** (local instance or LocalDB)
- **Visual Studio 2022** or **VS Code**

---

## 🧱 Solution Structure

```
src/
 ├─ AlzaProductApi.Web            // ASP.NET Core Web API (startup project)
 ├─ AlzaProductApi.Core           // Domain models, interfaces, services
 └─ AlzaProductApi.Infrastructure // EF Core, DbContext, repositories
 ```
---

## ▶️ Running the Application
### 1️⃣ Configure database connection

Edit appsettings.Development.json in **AlzaProductApi.Web**:
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=AlzaProductApi;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

---

### 2️⃣ Create database using EF Core migrations

Run the following commands (Package Manager Console or CLI):
```
Add-Migration InitialCreate -Project AlzaProductApi.Infrastructure -StartupProject AlzaProductApi.Web
Update-Database -Project AlzaProductApi.Infrastructure -StartupProject AlzaProductApi.Web
```
This will:
- 🗄️ create the database
- 📋 create tables
- 🌱 insert sample seed data

---

### 3️⃣ Run the application

Start the **AlzaProductApi.Web** project.

Swagger UI will be available at:
```
https://localhost:7049/swagger
```

---

## 🔀 API Versioning

The API uses **URL-based versioning**.

**Available versions**
- **v1** – basic endpoints, no pagination
- **v2** – paginated product listing

**Example URLs**
```
GET /api/v1/products
GET /api/v2/products?page=1&pageSize=10
```

---

### 📄 Pagination (v2)

The **v2** product endpoint returns:
- **Response body** – list of products
- **HTTP headers** – pagination metadata

**Pagination headers**
```
X-Total-Count
X-Page
X-Page-Size
Link (first / prev / next / last)
```
**Example request**
```
GET /api/v2/products?page=1&pageSize=5
```

---


## 📝 Notes

- API versioning is **independent of deployment**
- **Docker is intentionally not used** (not required by the assignment)
- The solution is prepared for:
   - additional API versions
   - containerization
   - CI/CD pipelines
- Swagger is configured to expose **versioned endpoints**

---

## 🛠 Technologies

- **ASP.NET Core** (.NET 8)
- **Entity Framework Core**
- **SQL Server**
- **Swashbuckle / Swagger**
- **ASP.NET API Versioning**

---

## ✅ Running Unit Tests

PowerShell - from the root of the solution:
```
dotnet test
```
