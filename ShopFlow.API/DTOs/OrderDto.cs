// [CONCEPT] OrderDto — the outbound shape for Order data including line items.
// [WHY] Nested DTOs (OrderItemDto inside OrderDto) flatten what would otherwise be complex
//       JOINed data into a single clean response object for the Angular frontend.

namespace ShopFlow.API.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
}

public class OrderItemDto
{
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
