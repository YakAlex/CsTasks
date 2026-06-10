using System.ComponentModel.DataAnnotations;

namespace WebApiLab.DTOs;

public class ProductCreateDto
{
    [Required(ErrorMessage = "Назва є обов'язковою")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Назва від 2 до 100 символів")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Опис не більше 500 символів")]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, 999999.99, ErrorMessage = "Ціна повинна бути від 0.01 до 999999.99")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Кількість не може бути від'ємною")]
    public int Stock { get; set; }
}