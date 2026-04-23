// [CONCEPT] OrderRepository — data access for Orders, including their OrderItems and Products.
// [WEEK-2] EF Core: Include() performs a SQL JOIN to load related data in one query.
// [WEEK-3] SQL: Include() → LEFT JOIN OrderItems ON Orders.Id = OrderItems.OrderId

using Microsoft.EntityFrameworkCore;
using ShopFlow.API.Data;
using ShopFlow.API.Models;
using ShopFlow.API.Repositories.Interfaces;

namespace ShopFlow.API.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ShopFlowDbContext _context;

    public OrderRepository(ShopFlowDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        // [CONCEPT] Include() = SQL JOIN — loads the Customer name alongside each Order.
        // [WEEK-2] Without Include(), Customer would be null (EF Core lazy loading is off by default).
        return await _context.Orders
            .Include(o => o.Customer)
            .ToListAsync();
    }

    public async Task<Order?> GetByIdWithItemsAsync(int id)
    {
        // [CONCEPT] ThenInclude() — nested JOIN. Order → OrderItems → Product (2 levels deep).
        // [WEEK-3] SQL equivalent: Orders JOIN OrderItems JOIN Products — two JOINs in one query.
        return await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Include(o => o.Customer)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId)
    {
        return await _context.Orders
            .Where(o => o.CustomerId == customerId)
            .Include(o => o.OrderItems)
            .ToListAsync();
    }

    public async Task<Order> CreateAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<Order?> UpdateStatusAsync(int id, string status)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order is null) return null;

        // [CONCEPT] Calling domain method — not directly setting Status.
        // [WEEK-1] Encapsulation: Status has private set. UpdateStatus() is the only legal way to change it.
        order.UpdateStatus(status);
        await _context.SaveChangesAsync();
        return order;
    }
}
