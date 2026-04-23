// [CONCEPT] A Model (Entity) represents a database table as a C# class.
// [CONCEPT] Each property maps to a column. EF Core reads this class to create/query the table.
// [WHY] We separate Models (DB shape) from DTOs (API shape) — the DB can evolve without breaking the API contract.
// [WEEK-1] OOP — this is a plain C# class using encapsulation (private setters where needed).
// [WEEK-2] EF Core reads this class to generate SQL migrations.

namespace ShopFlow.API.Models;

public class Product
{
    // [CONCEPT] EF Core convention: property named "Id" auto-becomes the Primary Key.
    public int Id { get; set; }

    // [CONCEPT] required keyword (C# 11+) enforces non-null at compile time.
    public required string Name { get; set; }

    public string? Description { get; set; }

    // [CONCEPT] decimal is used for money — never use float/double for currency due to rounding errors.
    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public string? Category { get; set; }

    // [CONCEPT] CreatedAt records when the row was inserted — useful for auditing and sorting.
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // [CONCEPT] Navigation property — EF Core uses this to load related OrderItems (one Product → many OrderItems).
    // [WEEK-2] This is how EF Core handles relationships without writing SQL JOINs manually.
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
