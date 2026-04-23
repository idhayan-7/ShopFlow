// [CONCEPT] Order service interface — defines order management operations.
// [WEEK-2] Service layer abstraction. Controller depends on this, not on OrderService directly.

using ShopFlow.API.DTOs;

namespace ShopFlow.API.Services.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
    Task<OrderDto?> GetOrderByIdAsync(int id);
    Task<OrderDto> CreateOrderAsync(CreateOrderDto dto);
    Task<OrderDto?> UpdateOrderStatusAsync(int id, string status);
}
