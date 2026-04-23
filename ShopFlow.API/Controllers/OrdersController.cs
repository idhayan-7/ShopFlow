// [CONCEPT] OrdersController — handles all HTTP operations for Orders.
// [WEEK-7 PLACEHOLDER] JWT Auth: Add [Authorize] attribute here in Week 7 to protect these endpoints.

using Microsoft.AspNetCore.Mvc;
using ShopFlow.API.DTOs;
using ShopFlow.API.Services.Interfaces;

namespace ShopFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
// [WEEK-7 PLACEHOLDER] [Authorize] — uncomment when JWT auth is added in Week 7
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // GET /api/orders
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return Ok(orders);
    }

    // GET /api/orders/5
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetById(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        return order is null ? NotFound() : Ok(order);
    }

    // POST /api/orders
    [HttpPost]
    public async Task<ActionResult<OrderDto>> Create([FromBody] CreateOrderDto dto)
    {
        var created = await _orderService.CreateOrderAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PATCH /api/orders/5/status
    // [CONCEPT] PATCH is used for partial updates — we only change the Status field here.
    // [WHY] PUT replaces the whole resource. PATCH updates one field. Status change = PATCH.
    [HttpPatch("{id}/status")]
    public async Task<ActionResult<OrderDto>> UpdateStatus(int id, [FromBody] string status)
    {
        var updated = await _orderService.UpdateOrderStatusAsync(id, status);
        return updated is null ? NotFound() : Ok(updated);
    }
}
