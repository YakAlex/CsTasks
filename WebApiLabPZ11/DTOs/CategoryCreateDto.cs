using System.ComponentModel.DataAnnotations;

namespace WebApiLab.DTOs;

public class CategoryCreateDto
{
    [Required(ErrorMessage = "Назва є обов'язковою")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Назва від 2 до 50 символів")]
    public string Name { get; set; } = string.Empty;

    [StringLength(200, ErrorMessage = "Опис не більше 200 символів")]
    public string Description { get; set; } = string.Empty;
}