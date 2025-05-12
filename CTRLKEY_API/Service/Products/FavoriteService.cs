using CTRLKEY_API.Data;
using CTRLKEY_API.Models.Tokens;
using Microsoft.EntityFrameworkCore;

namespace CTRLKEY_API.Service.Products;

public class FavoriteService
{
    private readonly DBContext _database;

    public FavoriteService(DBContext database)
    {
        _database = database;
    }
    
    //
    public async Task<Favorite> AddFavorite(int userId, int productId)
    {
        var existingFavorite = await _database.Favorites.FirstOrDefaultAsync(f => f.UserId == userId && f.ProductId == productId);
        if (existingFavorite != null)
        {
            return null;
        }

        var favorite = new Favorite
        {
            UserId = userId,
            ProductId = productId
        };
        
        _database.Favorites.Add(favorite);
        await _database.SaveChangesAsync();
        return favorite;    

    }
    
    //
    public async Task<bool> RemoveFavorite(int userId, int productId)
    {
        var favorite = await _database.Favorites.FirstOrDefaultAsync(f => f.UserId == userId && f.ProductId == productId);
        if (favorite == null)
        {
            return false;
        }
        _database.Favorites.Remove(favorite);
        await _database.SaveChangesAsync();
        return true;   
    }
    
    // 
    public async Task<List<Product>> GetFavoritesByUserId(int userId)
    {
        var favoriteProductIds = await _database.Favorites
            .Where(f => f.UserId == userId)
            .Select(f => f.ProductId)
            .ToListAsync();  

        var favoriteProducts = await _database.Products
            .Where(p => favoriteProductIds.Contains(p.Id))  
            .ToListAsync();
        return favoriteProducts;
    }

    
}