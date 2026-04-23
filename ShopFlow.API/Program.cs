// [CONCEPT] Program.cs is the entry point of an ASP.NET Core app.
// [CONCEPT] builder.Services is where Dependency Injection (DI) registrations happen.
// [WHY] We register services here so the DI container can inject them automatically — no manual "new" keyword needed anywhere.
// [WEEK-2] Dependency Injection — Scoped means one instance per HTTP request.

using Microsoft.EntityFrameworkCore;
using ShopFlow.API.Data;
using ShopFlow.API.Repositories.Interfaces;
using ShopFlow.API.Repositories;
using ShopFlow.API.Services.Interfaces;
using ShopFlow.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// [CONCEPT] DbContext registered as Scoped — one instance per request, which is correct for EF Core.
// [WHY] Scoped lifetime matches the HTTP request lifecycle, preventing shared state between requests.
// [WEEK-2] Entity Framework Core — DbContext is the bridge between C# and SQL Server.
builder.Services.AddDbContext<ShopFlowDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// [CONCEPT] Repository Pattern — abstracts database logic behind an interface.
// [WHY] Controllers and Services should NOT know how data is stored. They only talk to the interface.
// [WEEK-2] Scoped lifetime ensures each request gets its own repository instance.
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// [CONCEPT] Service Layer — business logic lives here, not in Controllers.
// [WHY] Controllers should only handle HTTP in/out. Services handle the "what should happen" logic.
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// [CONCEPT] CORS policy — allows the Angular frontend (different port) to call this API.
// [WEEK-4] Angular runs on localhost:4200, API runs on localhost:5000 — they are different "origins".
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngular");

// [WEEK-7 PLACEHOLDER] app.UseMiddleware<GlobalErrorHandlingMiddleware>();
// Uncomment above when global error handling is added in Week 7.

app.UseAuthorization();
app.MapControllers();
app.Run();
