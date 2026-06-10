using Microsoft.AspNetCore.Mvc;
using WebApiLab.DTOs;
using WebApiLab.Services;

namespace WebApiLab.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// Отримати список усіх товарів
    /// </summary>
    /// <returns>Список товарів</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }

    /// <summary>
    /// Отримати товар за ідентифікатором
    /// </summary>
    /// <param name="id">Ідентифікатор товару</param>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product is null)
            return NotFound(new { message = $"Товар з ID={id} не знайдено" });

        return Ok(product);
    }

    /// <summary>
    /// Створити новий товар
    /// </summary>
    /// <param name="dto">Дані нового товару</param>
    [HttpPost]
    [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] ProductCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _productService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Оновити існуючий товар
    /// </summary>
    /// <param name="id">Ідентифікатор товару</param>
    /// <param name="dto">Нові дані товару</param>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _productService.UpdateAsync(id, dto);

        if (updated is null)
            return NotFound(new { message = $"Товар з ID={id} не знайдено" });

        return Ok(updated);
    }

    /// <summary>
    /// Видалити товар
    /// </summary>
    /// <param name="id">Ідентифікатор товару</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _productService.DeleteAsync(id);

        if (!deleted)
            return NotFound(new { message = $"Товар з ID={id} не знайдено" });

        return NoContent();
    }
}