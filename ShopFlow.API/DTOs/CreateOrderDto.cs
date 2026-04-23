// [CONCEPT] CreateOrderDto — inbound shape for creating a new order.
// [WHY] CustomerId and a list of items are all the client needs to provide.
//       TotalAmount is calculated server-side — never trusted from client input.

namespace ShopFlow.API.DTOs;

public class CreateOrderDto
{
    public int CustomerId { get; set; }
    public List<CreateOrderItemDto> Items { get; set; } = new();
}

public class CreateOrderItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
