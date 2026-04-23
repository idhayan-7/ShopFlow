# ShopFlow — E-Commerce Order Management System

A full-stack learning project built to demonstrate professional .NET and Angular development skills.

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Backend | ASP.NET Core 8 Web API |
| ORM | Entity Framework Core 8 |
| Database | SQL Server |
| Frontend | Angular 17 (Standalone) |
| Language | C# 12 / TypeScript 5 |

## Project Structure

```
ShopFlow/
├── ShopFlow.API/        # ASP.NET Core 8 Web API
├── shopflow-ui/         # Angular 17 frontend
└── ShopFlow.Database/   # SQL scripts and seed data
```

## Architecture

This project follows a 3-layer architecture:
- **Controller** → receives HTTP requests, returns responses
- **Service** → contains business logic
- **Repository** → handles all database operations via EF Core

## Features Implemented (Weeks 1–5)

- [x] Product CRUD (Create, Read, Update, Delete)
- [x] Order management with line items
- [x] EF Core migrations and relationships
- [x] Repository Pattern + Dependency Injection
- [x] Angular 17 standalone components
- [x] Angular Services + HttpClient calling the .NET API
- [x] Reactive Forms with validation
- [x] Angular Routing with Route Guards
- [x] HTTP Interceptors (JWT-ready placeholder)

## Upcoming Features

- [ ] Week 6: Azure deployment (App Service + Azure SQL + Key Vault)
- [ ] Week 7: JWT Authentication + Global Error Handling + CQRS
- [ ] Week 8: SignalR real-time updates + Unit Tests

## Getting Started

### Backend
```bash
cd ShopFlow.API
dotnet restore
dotnet ef database update
dotnet run
```

### Frontend
```bash
cd shopflow-ui
npm install
ng serve
```

API runs on: http://localhost:5000  
UI runs on: http://localhost:4200
