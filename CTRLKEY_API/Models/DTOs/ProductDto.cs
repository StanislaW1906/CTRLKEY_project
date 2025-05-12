using System;
using System.ComponentModel.DataAnnotations;

namespace CTRLKEY_API.Models.DTOs;

public class ProductDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; } = string.Empty;

    [Required]
    public double Price { get; set; }

    [Required]
    public int Stock { get; set; }

    [Required]
    public string ImageUrl { get; set; } = string.Empty;

    [Required]
    public string TypeProduct { get; set; }

}

