// [CONCEPT] DTO (Data Transfer Object) — the shape of data sent OUT of the API to the client.
// [WHY] DTOs decouple the API contract from the database schema.
//       The DB can have extra columns (audit fields, internal flags) that we never expose.
// [WEEK-2] Best practice: always return DTOs from API endpoints, never raw EF Core models.

namespace ShopFlow.API.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string? Category { get; set; }
    public DateTime CreatedAt { get; set; }
}
