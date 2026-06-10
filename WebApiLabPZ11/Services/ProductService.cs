using WebApiLab.DTOs;
using WebApiLab.Models;

namespace WebApiLab.Services;

public class ProductService : IProductService
{
    // In-memory сховище (імітує БД)
    private readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Ноутбук Dell XPS", Description = "15.6\", Core i7, 16GB RAM", Price = 45000, Stock = 10, CreatedAt = DateTime.UtcNow.AddDays(-30) },
        new Product { Id = 2, Name = "Миша Logitech MX", Description = "Бездротова ергономічна миша", Price = 2500, Stock = 50, CreatedAt = DateTime.UtcNow.AddDays(-15) },
        new Product { Id = 3, Name = "Монітор LG 27\"", Description = "4K IPS, 144Hz", Price = 18000, Stock = 5, CreatedAt = DateTime.UtcNow.AddDays(-7) },
    };

    private int _nextId = 4;

    public Task<IEnumerable<ProductResponseDto>> GetAllAsync()
    {
        var result = _products.Select(MapToDto);
        return Task.FromResult(result);
    }

    public Task<ProductResponseDto?> GetByIdAsync(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        var result = product is not null ? MapToDto(product) : null;
        return Task.FromResult(result);
    }

    public Task<ProductResponseDto> CreateAsync(ProductCreateDto dto)
    {
        var product = new Product
        {
            Id = _nextId++,
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Stock = dto.Stock,
            CreatedAt = DateTime.UtcNow
        };

        _products.Add(product);
        return Task.FromResult(MapToDto(product));
    }

    public Task<ProductResponseDto?> UpdateAsync(int id, ProductUpdateDto dto)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product is null)
            return Task.FromResult<ProductResponseDto?>(null);

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.Stock = dto.Stock;

        return Task.FromResult<ProductResponseDto?>(MapToDto(product));
    }

    public Task<bool> DeleteAsync(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product is null)
            return Task.FromResult(false);

        _products.Remove(product);
        return Task.FromResult(true);
    }

    // Маппінг моделі у DTO відповіді
    private static ProductResponseDto MapToDto(Product p) => new()
    {
        Id = p.Id,
        Name = p.Name,
        Description = p.Description,
        Price = p.Price,
        Stock = p.Stock,
        CreatedAt = p.CreatedAt
    };
}