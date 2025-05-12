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

    // POST: api/cart-items
    [HttpPost]
    public async Task<ActionResult> AddCartItems([FromBody] CartItemsDto dto)
    {
        var cartItems = await _cartItemsService.AddCartItem(dto.UserId, dto.ProductId, dto.Quantity);
        return Ok(cartItems);
    }

    // GET: api/cart-items/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult> GetCartItemsById(int id)
    {
        var cartItem = await _cartItemsService.GetCartItemById(id);
        if (cartItem == null)
        {
            return NotFound();
        }
        return Ok(cartItem);
    }

    // DELETE: api/cart-items/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCartItemsById(int id)
    {
        var cartItem = await _cartItemsService.DeleteCartItemById(id);
        if (cartItem == null)
        {
            return NotFound();
        }
        return Ok(cartItem);
    }

    // PUT: api/cart-items/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCartItems(int id, [FromBody] CartItemsDto dto)
    {
        var updatedCartItem = await _cartItemsService.UpdateCartItem(id , dto.Quantity);
        if (updatedCartItem == null)
        {
            return NotFound();
        }
        return Ok(updatedCartItem);
    }
}