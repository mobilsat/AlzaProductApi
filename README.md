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

```text
src/
 ├─ AlzaProductApi.Web            // ASP.NET Core Web API (startup project)
 ├─ AlzaProductApi.Core           // Domain models, interfaces, services
 └─ AlzaProductApi.Infrastructure // EF Core, DbContext, repositories
