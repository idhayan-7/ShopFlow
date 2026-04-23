// [CONCEPT] DbContext is the core EF Core class — it is the bridge between C# objects and SQL Server.
// [CONCEPT] Each DbSet<T> maps to a table. DbSet<Product> → Products table.
// [WHY] All database operations (SELECT, INSERT, UPDATE, DELETE) go through the DbContext.
// [WEEK-2] EF Core — DbContext + DbSet is how we query and save data without writing raw SQL.
// [WEEK-3] SQL Server runs underneath — EF Core translates LINQ into SQL queries automatically.

using Microsoft.EntityFrameworkCore;
using ShopFlow.API.Models;

namespace ShopFlow.API.Data;

public class ShopFlowDbContext : DbContext
{
    // [CONCEPT] Constructor injection — DbContextOptions injected by DI (registered in Program.cs).
    public ShopFlowDbContext(DbContextOptions<ShopFlowDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // [CONCEPT] Fluent API — configure table relationships, constraints, and column types here.
        // [WHY] Fluent API is preferred over Data Annotations for complex configurations — keeps models clean.

        // [CONCEPT] Configure decimal precision for Money fields.
        // [WHY] SQL Server needs explicit precision for decimal — default is too small for currency.
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Order>()
            .Property(o => o.TotalAmount)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.UnitPrice)
            .HasColumnType("decimal(18,2)");

        // [CONCEPT] Explicit relationship configuration — EF Core can infer most, but explicit is clearer.
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict); // [WHY] Restrict: don't delete products that have order history.

        base.OnModelCreating(modelBuilder);
    }
}
