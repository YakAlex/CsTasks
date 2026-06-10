using WebApiLab.DTOs;
using WebApiLab.Models;

namespace WebApiLab.Services;

public class CategoryService : ICategoryService
{
    private readonly List<Category> _categories = new()
    {
        new Category { Id = 1, Name = "Ноутбуки", Description = "Портативні комп'ютери" },
        new Category { Id = 2, Name = "Периферія", Description = "Миші, клавіатури, навушники" },
        new Category { Id = 3, Name = "Монітори", Description = "Дисплеї та екрани" },
    };

    private int _nextId = 4;

    public Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
    {
        var result = _categories.Select(MapToDto);
        return Task.FromResult(result);
    }

    public Task<CategoryResponseDto?> GetByIdAsync(int id)
    {
        var category = _categories.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(category is not null ? MapToDto(category) : null);
    }

    public Task<CategoryResponseDto> CreateAsync(CategoryCreateDto dto)
    {
        var category = new Category
        {
            Id = _nextId++,
            Name = dto.Name,
            Description = dto.Description
        };

        _categories.Add(category);
        return Task.FromResult(MapToDto(category));
    }

    private static CategoryResponseDto MapToDto(Category c) => new()
    {
        Id = c.Id,
        Name = c.Name,
        Description = c.Description
    };
}