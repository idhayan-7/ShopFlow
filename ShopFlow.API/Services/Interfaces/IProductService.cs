// [CONCEPT] Service Interface — defines business operations available to the Controller.
// [WHY] The Controller depends on this interface, not on ProductService directly.
//       This decoupling means we can unit test the Controller by mocking this interface (Week 8).
// [WEEK-1] OOP: Interface as abstraction — no implementation, only contract.

using ShopFlow.API.DTOs;

namespace ShopFlow.API.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<ProductDto> CreateProductAsync(CreateProductDto dto);
    Task<ProductDto?> UpdateProductAsync(int id, CreateProductDto dto);
    Task<bool> DeleteProductAsync(int id);
}
