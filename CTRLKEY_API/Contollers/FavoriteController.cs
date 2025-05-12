using CTRLKEY_API.Models.DTOs;
using CTRLKEY_API.Service.Products;
using Microsoft.AspNetCore.Mvc;

namespace CTRLKEY_API.Contollers;

[ApiController]
[Route("api/favorite")]
public class FavoriteController : ControllerBase
{
    private readonly FavoriteService _favoriteService;

    public FavoriteController(FavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }
    
    //
    [HttpPost("add-favorite")]
    public async Task<ActionResult> AddFavorite([FromBody] FavoritesDto dto)
    {
        var favorite = await _favoriteService.AddFavorite(dto.UserId, dto.ProductId);
        if (favorite == null)
        {
            return BadRequest("Product already exists");
        }
        return Ok(favorite);
    }
    
    //
    [HttpDelete("remove-favorite")]
    public async Task<ActionResult> RemoveFavorite([FromBody] FavoritesDto dto)
    {
        var favorite = await _favoriteService.RemoveFavorite(dto.UserId, dto.ProductId);
        if (!favorite)
        {
            return BadRequest("Product not exists");
        }
        
        return Ok();   
    }
    
    //
    [HttpGet("get-favorites")]
    public async Task<ActionResult> GetFavoritesByUserId(int userId)
    {
        var favorites = await _favoriteService.GetFavoritesByUserId(userId);
        return Ok(favorites);   
    }
}