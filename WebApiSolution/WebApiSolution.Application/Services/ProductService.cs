using AutoMapper;
using WebApiSolution.Application.DTOs;
using WebApiSolution.Application.Interfaces;
using WebApiSolution.Domain.Entities;

namespace WebApiSolution.Application.Services;

public class ProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
    }

    public async Task<ProductResponseDto?> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return product is null ? null : _mapper.Map<ProductResponseDto>(product);
    }

    public async Task<ProductResponseDto> CreateAsync(ProductCreateDto dto)
    {
        var product = _mapper.Map<Product>(dto);
        var created = await _productRepository.CreateAsync(product);
        var result = await _productRepository.GetByIdAsync(created.Id);
        return _mapper.Map<ProductResponseDto>(result!);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _productRepository.DeleteAsync(id);
    }
}