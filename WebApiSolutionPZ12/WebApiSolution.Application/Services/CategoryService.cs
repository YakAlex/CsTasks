using AutoMapper;
using WebApiSolution.Application.DTOs;
using WebApiSolution.Application.Interfaces;
using WebApiSolution.Domain.Entities;

namespace WebApiSolution.Application.Services;

public class CategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);
    }

    public async Task<CategoryResponseDto?> GetByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        return category is null ? null : _mapper.Map<CategoryResponseDto>(category);
    }

    public async Task<CategoryResponseDto> CreateAsync(CategoryCreateDto dto)
    {
        var category = _mapper.Map<Category>(dto);
        var created = await _categoryRepository.CreateAsync(category);
        return _mapper.Map<CategoryResponseDto>(created);
    }
}