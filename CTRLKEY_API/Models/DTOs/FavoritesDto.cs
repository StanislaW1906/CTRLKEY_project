using System.ComponentModel.DataAnnotations;
namespace CTRLKEY_API.Models.DTOs;

public class FavoritesDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
}