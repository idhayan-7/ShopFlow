// [CONCEPT] Controller — the HTTP entry point. It receives requests and returns responses.
// [CONCEPT] Controllers should be THIN — no business logic here. Just call the Service and return results.
// [WHY] If business logic lived in the Controller, we couldn't reuse it elsewhere (e.g. background jobs).
// [WEEK-2] 3-Layer Architecture: THIS is Layer 1 (Controller). It calls Layer 2 (Service).

using Microsoft.AspNetCore.Mvc;
using ShopFlow.API.DTOs;
using ShopFlow.API.Services.Interfaces;

namespace ShopFlow.API.Controllers;

// [CONCEPT] [ApiController] adds automatic model validation, binding, and error responses.
// [CONCEPT] [Route("api/[controller]")] → URL becomes /api/products (controller name, lowercase).
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    // [CONCEPT] Constructor injection — DI provides IProductService. We never call "new ProductService()".
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // GET /api/products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products); // [CONCEPT] Ok() = HTTP 200 with JSON body.
    }

    // GET /api/products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        // [CONCEPT] NotFound() = HTTP 404. Always return meaningful HTTP status codes.
        return product is null ? NotFound() : Ok(product);
    }

    // POST /api/products
    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductDto dto)
    {
        var created = await _productService.CreateProductAsync(dto);
        // [CONCEPT] CreatedAtAction = HTTP 201 with a Location header pointing to the new resource.
        // [WHY] REST convention: POST should return 201 Created, not 200 OK.
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT /api/products/5
    [HttpPut("{id}")]
    public async Task<ActionResult<ProductDto>> Update(int id, [FromBody] CreateProductDto dto)
    {
        var updated = await _productService.UpdateProductAsync(id, dto);
        return updated is null ? NotFound() : Ok(updated);
    }

    // DELETE /api/products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _productService.DeleteProductAsync(id);
        // [CONCEPT] NoContent() = HTTP 204. Standard response for successful DELETE with no body.
        return deleted ? NoContent() : NotFound();
    }
}
