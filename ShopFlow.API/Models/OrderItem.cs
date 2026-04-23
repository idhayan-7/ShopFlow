// [CONCEPT] OrderItem is a junction entity — it connects Orders and Products (many-to-many relationship).
// [WHY] One Order can contain many Products. One Product can appear in many Orders.
//       OrderItem is the bridge table that holds quantity and price at time of purchase.
// [WEEK-2] EF Core: this creates a bridge table with FKs to both Orders and Products.
// [WEEK-3] SQL: this maps to a JOIN table (Orders JOIN OrderItems JOIN Products).

namespace ShopFlow.API.Models;

public class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order? Order { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public int Quantity { get; set; }

    // [WHY] We store UnitPrice at time of order — product price may change later.
    //       This preserves historical accuracy of what the customer actually paid.
    public decimal UnitPrice { get; set; }
}
