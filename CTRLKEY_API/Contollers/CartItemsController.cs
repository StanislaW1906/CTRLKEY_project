using CTRLKEY_API.Models.DTOs;
using CTRLKEY_API.Service.Products;
using Microsoft.AspNetCore.Mvc;

namespace CTRLKEY_API.Contollers;

[ApiController]
[Route("api/cart-items")]
public class CartItemsController : ControllerBase
{
    private readonly CartItemsService _cartItemsService;
    
    public CartItemsController(CartItemsService cartItemsService)
    {
        _cartItemsService = cartItemsService;
    }
    
    //
    [HttpPost("add-cart-items")]
    public async Task<ActionResult> AddCartItems([FromBody] CartItemsDto dto)
    {
        var cartItams = await _cartItemsService.AddCartItem(dto.UserId, dto.ProductId, dto.Quantity);
        return Ok(cartItams);
    }
    
    //
    [HttpGet("get-cart-items/{id}")]
    public async Task<ActionResult> GetCartItemsById(int id)
    {
        var cartItam = await _cartItemsService.GetCartItemById(id);
        return Ok(cartItam);
    }
    
    //
    [HttpDelete("delete-cart-items/{id}")]
    public async Task<ActionResult> DeleteCartItemsById(int id)
    {
        var cartItam = await _cartItemsService.DeleteCartItemById(id);
        return Ok(cartItam);
    }
    
    //
    [HttpPut("update-cart-items")]
    public async Task<ActionResult> UpdateCartItemsById([FromBody] CartItemsDto dto)
    {
        
        return Ok();
    }
    
}