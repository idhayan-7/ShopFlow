// [CONCEPT] OrderService — business logic for creating and managing orders.
// [WHY] Creating an order involves multiple steps: calculate total, create order items, save.
//       That multi-step logic belongs here in the Service, not in the Controller.
// [WEEK-2] async/await throughout — never block a thread on DB calls.

using ShopFlow.API.DTOs;
using ShopFlow.API.Models;
using ShopFlow.API.Repositories.Interfaces;
using ShopFlow.API.Services.Interfaces;

namespace ShopFlow.API.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return orders.Select(MapToDto);
    }

    public async Task<OrderDto?> GetOrderByIdAsync(int id)
    {
        var order = await _orderRepository.GetByIdWithItemsAsync(id);
        return order is null ? null : MapToDto(order);
    }

    public async Task<OrderDto> CreateOrderAsync(CreateOrderDto dto)
    {
        // [CONCEPT] Business logic — calculate the total amount from line items.
        // [WHY] Total is computed server-side, not trusted from the client. Security best practice.
        decimal total = 0;
        var orderItems = new List<OrderItem>();

        foreach (var item in dto.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            if (product is null)
                throw new ArgumentException($"Product {item.ProductId} not found");

            // [WHY] We snapshot UnitPrice at order time — if price changes tomorrow, this order is unaffected.
            orderItems.Add(new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = product.Price
            });

            total += product.Price * item.Quantity;
        }

        var order = new Order
        {
            CustomerId = dto.CustomerId,
            TotalAmount = total,
            OrderItems = orderItems
        };

        var created = await _orderRepository.CreateAsync(order);
        return MapToDto(created);
    }

    public async Task<OrderDto?> UpdateOrderStatusAsync(int id, string status)
    {
        var updated = await _orderRepository.UpdateStatusAsync(id, status);
        return updated is null ? null : MapToDto(updated);
    }

    private static OrderDto MapToDto(Order order) => new()
    {
        Id = order.Id,
        CustomerId = order.CustomerId,
        CustomerName = order.Customer?.Name,
        Status = order.Status,
        TotalAmount = order.TotalAmount,
        OrderDate = order.OrderDate,
        Items = order.OrderItems?.Select(oi => new OrderItemDto
        {
            ProductId = oi.ProductId,
            ProductName = oi.Product?.Name,
            Quantity = oi.Quantity,
            UnitPrice = oi.UnitPrice
        }).ToList() ?? new List<OrderItemDto>()
    };
}
