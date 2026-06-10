using Microsoft.EntityFrameworkCore;
using WebApiSolution.Application.Interfaces;
using WebApiSolution.Domain.Entities;
using WebApiSolution.Infrastructure.Data;

namespace WebApiSolution.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product> CreateAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }
    
    public async Task<Product?> UpdateAsync(int id, Product updated)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null) return null;

        product.Name        = updated.Name;
        product.Description = updated.Description;
        product.Price       = updated.Price;
        product.Stock       = updated.Stock;
        product.CategoryId  = updated.CategoryId;

        await _context.SaveChangesAsync();

        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    
    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null) return false;
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}