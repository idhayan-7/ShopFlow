// [CONCEPT] Customer entity — represents the person placing orders.
// [WEEK-1] OOP — class with properties. No behavior here, just data shape.
// [WEEK-2] EF Core will create a Customers table from this class.

namespace ShopFlow.API.Models;

public class Customer
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // [CONCEPT] Navigation property — one Customer can have many Orders.
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
