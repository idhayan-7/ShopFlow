# CLAUDE.md — ShopFlow Project Instructions

> Place this file at: ShopFlow/CLAUDE.md
> Claude Code reads this automatically at the start of every session in this project.
> This replaces the need to re-explain the project context every time.

---

## PROJECT IDENTITY

**Name:** ShopFlow — E-Commerce Order Management System
**Purpose:** A learning project covering the full .NET stack. Every file has structured comments for interview preparation.
**Owner:** Idhayan (self-learning journey, Week 1–5 complete, Weeks 6–8 upcoming)

---

## TECH STACK

| Layer | Technology | Version |
|-------|-----------|---------|
| Backend | ASP.NET Core Web API | .NET 8 |
| ORM | Entity Framework Core | 8.x |
| Database | SQL Server | 2022 |
| Frontend | Angular (Standalone) | 17 |
| Language | C# / TypeScript | 12 / 5 |

---

## FOLDER STRUCTURE (NON-NEGOTIABLE)

```
ShopFlow/                          ← root — never add files directly here except README.md and CLAUDE.md
├── ShopFlow.API/                  ← ASP.NET Core 8 backend
│   ├── Controllers/
│   ├── Services/Interfaces/
│   ├── Repositories/Interfaces/
│   ├── Models/
│   ├── DTOs/
│   ├── Data/
│   └── Middleware/                ← empty until Week 7
├── shopflow-ui/                   ← Angular 17 frontend
│   └── src/app/
│       ├── components/
│       ├── services/
│       ├── models/
│       ├── guards/
│       └── interceptors/
├── ShopFlow.Database/             ← SQL scripts only
│   ├── migrations/
│   ├── seeds/
│   └── stored-procedures/
├── CLAUDE.md                      ← this file
└── README.md
```

**Never create files outside this structure without asking first.**

---

## ARCHITECTURE RULES (ALWAYS FOLLOW)

### Backend — 3-layer architecture (strict)
```
HTTP Request
    ↓
Controller        ← only handles HTTP in/out. No business logic. No DbContext.
    ↓
Service           ← all business logic lives here. No DbContext access.
    ↓
Repository        ← only EF Core operations. No business logic.
    ↓
DbContext → SQL Server
```

- Controllers NEVER access DbContext directly
- Services NEVER access DbContext directly
- Repositories NEVER contain business logic
- All layers communicate through interfaces (IProductService, IProductRepository, etc.)

### Frontend — Angular component → service → HttpClient
```
Component (UI logic only)
    ↓
Service (HTTP calls only)
    ↓
.NET API
```

- Components NEVER call HttpClient directly — always through a service
- Services return Observables — components subscribe

---

## COMMENT STANDARD (MANDATORY IN EVERY FILE)

Every file MUST have structured comments. Use exactly these tags:

```csharp
// [CONCEPT] Explains the pattern/technique — this is the interview talking point
// [WHY]     Explains the specific decision made in THIS file — not generic
// [WEEK-N]  Which week this was learned (1–8) — marks scalability points
// [WEEK-N PLACEHOLDER] Code that will be added in a future week — do NOT implement yet
```

### Examples of GOOD comments:
```csharp
// [CONCEPT] Repository Pattern — abstracts database logic behind an interface.
// [WHY] Services depend on IProductRepository, not ProductRepository, so we can mock it in tests.
// [WEEK-2] DI registration in Program.cs: AddScoped<IProductRepository, ProductRepository>()
// [WEEK-7 PLACEHOLDER] app.UseMiddleware<GlobalErrorHandlingMiddleware>(); — add when Week 7 is done
```

### Examples of BAD comments (never write these):
```csharp
// Gets all products        ← states the obvious
// This is a controller     ← restates the class name
// TODO: fix later          ← vague, unhelpful
```

---

## NAMING CONVENTIONS

### C# (Backend)
| Type | Convention | Example |
|------|-----------|---------|
| Classes | PascalCase | `ProductService` |
| Interfaces | IPascalCase | `IProductService` |
| Methods | PascalCase | `GetAllProductsAsync()` |
| Private fields | _camelCase | `_productRepository` |
| DTOs | Suffix with Dto | `ProductDto`, `CreateProductDto` |
| Always async | Suffix with Async | `GetAllAsync()`, `CreateAsync()` |

### TypeScript (Frontend)
| Type | Convention | Example |
|------|-----------|---------|
| Interfaces | PascalCase | `Product`, `Order` |
| Services | PascalCase + Service | `ProductService` |
| Components | PascalCase + Component | `ProductListComponent` |
| Methods | camelCase | `loadProducts()`, `onSubmit()` |
| Observables | $ suffix | `products$` (optional but encouraged) |

### Files
- C# files: `PascalCase.cs` → `ProductService.cs`
- Angular files: `kebab-case.component.ts` → `product-list.component.ts`
- SQL files: `NNN_PascalCase.sql` → `001_InitialSchema.sql`

---

## EF CORE RULES

1. **Always use async methods** — `ToListAsync()`, `FindAsync()`, `SaveChangesAsync()`. Never synchronous.
2. **Decimal columns must specify precision** — `HasColumnType("decimal(18,2)")` in `OnModelCreating`.
3. **Never use lazy loading** — always use explicit `Include()` / `ThenInclude()`.
4. **Migrations go in ShopFlow.API** — run `dotnet ef migrations add <Name>` from `ShopFlow.API/`.
5. **Never modify migration files manually** — if schema changes, add a new migration.

```bash
# Always run from ShopFlow.API/ folder:
dotnet ef migrations add <MigrationName>
dotnet ef database update
dotnet ef migrations remove   # only if migration was NOT applied yet
```

---

## ANGULAR RULES

1. **Standalone components only** — no NgModule. Use `standalone: true` in every `@Component`.
2. **Always import dependencies in the component** — `imports: [CommonModule, RouterModule, ReactiveFormsModule]`
3. **Use Reactive Forms** — never Template-driven forms for order/product forms.
4. **HttpClient returns Observables** — never convert to Promise unless explicitly asked.
5. **Unsubscribe on destroy** — for subscriptions in components, use `takeUntilDestroyed()` or unsubscribe in `ngOnDestroy`.

---

## WEEK-BY-WEEK FEATURE FLAGS

Track what is implemented vs placeholder:

| Feature | Week | Status | File Location |
|---------|------|--------|---------------|
| Product CRUD API | 1–2 | ✅ Done | `Controllers/ProductsController.cs` |
| Order management API | 1–2 | ✅ Done | `Controllers/OrdersController.cs` |
| EF Core + SQL Server | 2–3 | ✅ Done | `Data/ShopFlowDbContext.cs` |
| Repository Pattern | 2 | ✅ Done | `Repositories/` |
| Angular Components | 4 | ✅ Done | `shopflow-ui/src/app/components/` |
| Angular Services + HttpClient | 4 | ✅ Done | `shopflow-ui/src/app/services/` |
| Routing + Guards | 4–5 | ✅ Done | `app.routes.ts`, `guards/auth.guard.ts` |
| Reactive Forms | 5 | ✅ Done | `order-form.component.ts` |
| HTTP Interceptors | 5 | ✅ Placeholder | `interceptors/auth.interceptor.ts` |
| Azure Deployment | 6 | ⏳ Upcoming | `appsettings.json` + GitHub Actions |
| JWT Auth | 7 | ⏳ Upcoming | `Middleware/`, `Controllers/` |
| Global Error Handling | 7 | ⏳ Upcoming | `Middleware/GlobalErrorHandlingMiddleware.cs` |
| CQRS + MediatR | 7 | ⏳ Upcoming | New `Commands/` + `Queries/` folders |
| SignalR | 8 | ⏳ Upcoming | New `Hubs/` folder |
| Unit Tests (.NET) | 8 | ⏳ Upcoming | New `ShopFlow.Tests/` project |
| Unit Tests (Angular) | 8 | ⏳ Upcoming | `*.spec.ts` files |

---

## WEEK 6 — HOW TO EXTEND (Azure)

When Week 6 is complete, add these without touching existing files:

```
ShopFlow/
├── .github/
│   └── workflows/
│       └── deploy.yml              ← GitHub Actions CI/CD pipeline
├── ShopFlow.API/
│   ├── appsettings.Production.json ← Azure connection strings (no secrets here)
│   └── (no changes to existing files)
```

Update `Program.cs` only to add Key Vault configuration — add at the top of the builder setup block marked `[WEEK-6 PLACEHOLDER]`.

---

## WEEK 7 — HOW TO EXTEND (JWT + CQRS)

```
ShopFlow.API/
├── Middleware/
│   └── GlobalErrorHandlingMiddleware.cs   ← uncomment the line in Program.cs
├── Commands/
│   ├── CreateProductCommand.cs
│   └── CreateOrderCommand.cs
├── Queries/
│   ├── GetAllProductsQuery.cs
│   └── GetOrderByIdQuery.cs
└── Auth/
    ├── JwtService.cs
    └── LoginDto.cs
```

Add `[Authorize]` attribute to `OrdersController` — line is already commented and marked `[WEEK-7 PLACEHOLDER]`.
Replace `true` in `auth.guard.ts` with real JWT check — line is already marked `[WEEK-7 PLACEHOLDER]`.

---

## WEEK 8 — HOW TO EXTEND (SignalR + Tests)

```
ShopFlow.API/
└── Hubs/
    └── OrderStatusHub.cs           ← new file

ShopFlow.Tests/                     ← new .NET test project (add to solution)
├── ProductServiceTests.cs
└── ProductsControllerTests.cs

shopflow-ui/src/app/
└── components/order-list/
    └── order-list.component.spec.ts ← new test file
```

---

## COMMON COMMANDS REFERENCE

```bash
# ── BACKEND ──────────────────────────────────────────
cd ShopFlow/ShopFlow.API

dotnet run                          # Start API (http://localhost:5000)
dotnet build                        # Build without running
dotnet watch run                    # Hot reload during development

dotnet ef migrations add <Name>     # Create new EF Core migration
dotnet ef database update           # Apply pending migrations
dotnet ef migrations list           # See all migrations and their status

# ── FRONTEND ─────────────────────────────────────────
cd ShopFlow/shopflow-ui

ng serve                            # Start dev server (http://localhost:4200)
ng generate component components/<name>   # Scaffold new component
ng generate service services/<name>       # Scaffold new service
ng build                            # Production build
ng test                             # Run Jasmine unit tests (Week 8)

# ── DATABASE ─────────────────────────────────────────
# Run SQL files manually in SSMS or Azure Data Studio:
# 1. ShopFlow.Database/migrations/001_InitialSchema.sql
# 2. ShopFlow.Database/seeds/001_SeedData.sql
```

---

## IF CLAUDE CODE ASKS A QUESTION

When Claude Code is uncertain about implementation, always answer with:
1. **Follow the 3-layer architecture** — Controller → Service → Repository
2. **Use async/await** everywhere database calls are involved
3. **Return DTOs from controllers** — never raw Model objects
4. **Add [CONCEPT], [WHY], [WEEK-N] comments** to every new file
5. **Check the Feature Flags table above** — if it says Placeholder, do not implement it

---

## WHAT NOT TO DO (CRITICAL)

- Do NOT add business logic to Controllers
- Do NOT access DbContext from Services
- Do NOT use synchronous EF Core methods (no `.ToList()`, only `.ToListAsync()`)
- Do NOT use `float` or `double` for money fields — always `decimal`
- Do NOT add new NuGet packages without asking first
- Do NOT modify existing migration files
- Do NOT implement Week 6–8 features until those weeks are reached
- Do NOT use NgModule in Angular — standalone components only
