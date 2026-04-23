// [CONCEPT] Repository — concrete implementation of IProductRepository.
// [CONCEPT] All SQL Server communication happens here through EF Core's DbContext.
// [WHY] If we move from SQL Server to PostgreSQL later, we only change this file — nothing else.
// [WEEK-2] EF Core: ToListAsync(), FindAsync(), Add(), Remove(), SaveChangesAsync() are EF Core methods.
// [WEEK-2] async/await: all DB calls are async to avoid blocking the thread.

using Microsoft.EntityFrameworkCore;
using ShopFlow.API.Data;
using ShopFlow.API.Models;
using ShopFlow.API.Repositories.Interfaces;

namespace ShopFlow.API.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ShopFlowDbContext _context;

    // [CONCEPT] Constructor Injection — DbContext injected by DI. We never create it manually.
    // [WEEK-1] Constructor type: Primary Constructor (standard DI pattern in .NET).
    public ProductRepository(ShopFlowDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        // [CONCEPT] ToListAsync() executes SELECT * FROM Products as an async SQL query.
        // [WEEK-2] IQueryable stays as a query until ToListAsync() is called — then it hits the DB.
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        // [CONCEPT] FindAsync is optimised for PK lookups — checks EF Core's local cache first.
        // [WHY] FindAsync is faster than FirstOrDefaultAsync when querying by primary key.
        return await _context.Products.FindAsync(id);
    }

    public async Task<Product> CreateAsync(Product product)
    {
        // [CONCEPT] Add() stages the entity in EF Core's change tracker.
        //           SaveChangesAsync() sends the actual INSERT SQL to SQL Server.
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> UpdateAsync(Product product)
    {
        var existing = await _context.Products.FindAsync(product.Id);
        if (existing is null) return null;

        // [CONCEPT] Update() marks ALL properties as modified — EF Core generates UPDATE SQL for the whole row.
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(string category)
    {
        // [CONCEPT] LINQ Where() — EF Core translates this to SQL: WHERE Category = @category
        // [WEEK-2] LINQ: method syntax. Stays as IQueryable until ToListAsync() is called.
        return await _context.Products
            .Where(p => p.Category == category)
            .ToListAsync();
    }
}
