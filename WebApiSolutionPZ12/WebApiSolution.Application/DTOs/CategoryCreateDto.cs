using System.ComponentModel.DataAnnotations;

namespace WebApiSolution.Application.DTOs;

public class CategoryCreateDto
{
    [Required(ErrorMessage = "Назва є обов'язковою")]
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [StringLength(200)]
    public string Description { get; set; } = string.Empty;
}