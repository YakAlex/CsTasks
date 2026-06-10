using Microsoft.EntityFrameworkCore;
using WebApiSolution.Domain.Entities;

namespace WebApiSolution.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Category seed data
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Ноутбуки", Description = "Портативні комп'ютери" },
            new Category { Id = 2, Name = "Периферія", Description = "Миші, клавіатури" },
            new Category { Id = 3, Name = "Монітори", Description = "Дисплеї та екрани" }
        );

        // Product seed data
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Dell XPS 15", Description = "Core i7, 16GB", Price = 45000, Stock = 10, CategoryId = 1, CreatedAt = DateTime.UtcNow },
            new Product { Id = 2, Name = "Logitech MX", Description = "Бездротова миша", Price = 2500, Stock = 50, CategoryId = 2, CreatedAt = DateTime.UtcNow },
            new Product { Id = 3, Name = "LG 27\" 4K", Description = "IPS, 144Hz", Price = 18000, Stock = 5, CategoryId = 3, CreatedAt = DateTime.UtcNow }
        );
    }
}