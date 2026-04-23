// [CONCEPT] Interface defines the CONTRACT — what operations are available, not how they work.
// [WHY] Services depend on this interface, not on the concrete class.
//       This makes it easy to swap the real DB for a mock in unit tests (Week 8).
// [WEEK-1] OOP — Interface is a pure abstraction. No implementation here.
// [WEEK-2] Repository Pattern — separates data access from business logic.

using ShopFlow.API.Models;

namespace ShopFlow.API.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product> CreateAsync(Product product);
    Task<Product?> UpdateAsync(Product product);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Product>> GetByCategoryAsync(string category);
}
