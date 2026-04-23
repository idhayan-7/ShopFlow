// [CONCEPT] Interface for Order data access — defines what the service layer can do with orders.
// [WEEK-2] Repository Pattern — service never talks to DbContext directly.

using ShopFlow.API.Models;

namespace ShopFlow.API.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order?> GetByIdWithItemsAsync(int id);
    Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId);
    Task<Order> CreateAsync(Order order);
    Task<Order?> UpdateStatusAsync(int id, string status);
}
