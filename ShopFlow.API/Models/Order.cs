// [CONCEPT] Order entity — the core business object in e-commerce.
// [WHY] We store OrderStatus as a string enum-like value so it's readable in the DB (not just 0,1,2).
// [WEEK-1] Encapsulation — Status has a private setter to prevent direct external modification.
// [WEEK-2] EF Core relationship: one Order belongs to one Customer, one Order has many OrderItems.

namespace ShopFlow.API.Models;

public class Order
{
    public int Id { get; set; }

    // [CONCEPT] Foreign Key — links this Order to a Customer row in the Customers table.
    public int CustomerId { get; set; }

    // [CONCEPT] Navigation property for the FK — EF Core uses this to JOIN the tables.
    public Customer? Customer { get; set; }

    // [CONCEPT] Status stored as string — readable values: "Pending", "Processing", "Shipped", "Delivered", "Cancelled"
    // [WEEK-1] private set — Status should only change through a method, not direct assignment from outside.
    public string Status { get; private set; } = "Pending";

    public decimal TotalAmount { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    // [CONCEPT] Navigation property — one Order contains many OrderItems.
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    // [CONCEPT] Domain method — business logic belongs on the entity, not scattered in services.
    // [WEEK-1] Encapsulation in action — the only way to change Status is through this method.
    public void UpdateStatus(string newStatus)
    {
        var validStatuses = new[] { "Pending", "Processing", "Shipped", "Delivered", "Cancelled" };
        if (!validStatuses.Contains(newStatus))
            throw new ArgumentException($"Invalid status: {newStatus}");
        Status = newStatus;
    }
}
