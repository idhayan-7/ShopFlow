// [CONCEPT] CreateProductDto — the shape of data coming IN from the client to create a product.
// [WHY] Separating Create DTO from response DTO means we control exactly what fields are writable.
//       For example, Id and CreatedAt are NOT in this DTO — client cannot set them.
// [WEEK-2] Validation attributes can be added here (e.g. [Required], [Range]) — Week 5 territory.

using System.ComponentModel.DataAnnotations;

namespace ShopFlow.API.DTOs;

public class CreateProductDto
{
    [Required]
    [StringLength(200, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    [Range(0.01, 999999.99, ErrorMessage = "Price must be between 0.01 and 999999.99")]
    public decimal Price { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
    public int StockQuantity { get; set; }

    public string? Category { get; set; }
}
