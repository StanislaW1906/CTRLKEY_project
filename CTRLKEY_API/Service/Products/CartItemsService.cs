using CTRLKEY_API.Data;
using CTRLKEY_API.Models.Products;

namespace CTRLKEY_API.Service.Products;

public class CartItemsService
{
    private readonly DBContext _database;
    
    public CartItemsService(DBContext database)
    {
        _database = database;
    }
    
    //
    public async Task<CartItems> AddCartItem(int userId, int productId, int quantity)
    {
        var cartItems = new CartItems
        {
            UserId = userId,
            ProductId = productId,
            Quantity = quantity
        };
        
        await _database.CartItems.AddAsync(cartItems);
        await _database.SaveChangesAsync();
        return cartItems;  
    }
    
    //
    public async Task<CartItems> DeleteCartItemById(int id)
    {
        var cartItems = await _database.CartItems.FindAsync(id);
        if (cartItems == null)
        {
            return null;
        }
        
        _database.CartItems.Remove(cartItems);
        await _database.SaveChangesAsync();
        return cartItems;
    }
    
    //
    public async Task<CartItems> GetCartItemById(int id)
    {
        var cartItem = await _database.CartItems.FindAsync(id);
        return cartItem;   
    }
    
    //
    public async Task<CartItems> UpdateCartItem(int id, CartItems cartItems)
    {
        var cartItem = await _database.CartItems.FindAsync(id);
        if (cartItem == null)
        {
            return null;
        }
        cartItem.UserId = cartItems.UserId;
        cartItem.ProductId = cartItems.ProductId;
        cartItem.Quantity = cartItems.Quantity;
        
        _database.CartItems.Update(cartItem);
        await _database.SaveChangesAsync();
        return cartItem;
    }
    
    
}