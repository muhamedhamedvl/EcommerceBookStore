﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BookShoppingCartMvcUI.Models.DTOs;
public class BookDTO
{
    public int Id { get; set; }

    [Required]
    [MaxLength(40)]
    public string? BookName { get; set; }

    [Required]
    [MaxLength(40)]
    public string? AuthorName { get; set; }

    [Required]
    public double Price { get; set; }

    [MaxLength(10000)]
    public string? Description { get; set; }

    [MaxLength(100)]
    public string? Quote { get; set; }

    public string? Image { get; set; }

    [Required]
    public int GenreId { get; set; }
    public IFormFile? ImageFile { get; set; }
    public IEnumerable<SelectListItem>? GenreList { get; set; }
}