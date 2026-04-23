// [CONCEPT] Service Layer — contains business logic. Lives between Controller and Repository.
// [WHY] Controllers should be thin (only HTTP in/out). All business decisions happen here.
// [CONCEPT] DTOs (Data Transfer Objects) are used here — we never return raw Model objects to the API.
// [WHY] DTOs let us control exactly what fields are exposed. E.g. we can hide internal fields.
// [WEEK-2] 3-Layer architecture: Controller → Service → Repository. This is the Service.

using ShopFlow.API.DTOs;
using ShopFlow.API.Models;
using ShopFlow.API.Repositories.Interfaces;
using ShopFlow.API.Services.Interfaces;

namespace ShopFlow.API.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    // [CONCEPT] Constructor injection — DI provides IProductRepository automatically.
    // [WEEK-2] Dependency Injection: we never write "new ProductRepository()" here.
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();
        // [CONCEPT] LINQ Select() — transforms each Product model into a ProductDto.
        // [WHY] We project to DTOs so we never accidentally expose internal model properties.
        return products.Select(MapToDto);
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return product is null ? null : MapToDto(product);
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto dto)
    {
        // [CONCEPT] Mapping DTO → Model. The incoming API shape is different from the DB shape.
        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            StockQuantity = dto.StockQuantity,
            Category = dto.Category
        };

        var created = await _productRepository.CreateAsync(product);
        return MapToDto(created);
    }

    public async Task<ProductDto?> UpdateProductAsync(int id, CreateProductDto dto)
    {
        var existing = await _productRepository.GetByIdAsync(id);
        if (existing is null) return null;

        existing.Name = dto.Name;
        existing.Description = dto.Description;
        existing.Price = dto.Price;
        existing.StockQuantity = dto.StockQuantity;
        existing.Category = dto.Category;

        var updated = await _productRepository.UpdateAsync(existing);
        return updated is null ? null : MapToDto(updated);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        return await _productRepository.DeleteAsync(id);
    }

    // [CONCEPT] Private mapping method — avoids repeating DTO mapping logic in every method.
    // [WHY] Single Responsibility: if the DTO shape changes, we only update this one method.
    private static ProductDto MapToDto(Product product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Description = product.Description,
        Price = product.Price,
        StockQuantity = product.StockQuantity,
        Category = product.Category,
        CreatedAt = product.CreatedAt
    };
}
