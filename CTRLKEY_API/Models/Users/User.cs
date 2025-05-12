using CTRLKEY_API.Models.Orders;
using CTRLKEY_API.Models.Products;
using CTRLKEY_API.Models.Tokens;

namespace CTRLKEY_API.Models.Users;

public class User
{
   
    public int Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; }
    
    public string? FullName { get; set; }
    public string? City { get; set; }
    public string? NovaPoshtaAddress { get; set; }
    public string? PhoneNumber { get; set; }
    
    public List<Product> Products { get; set; } = new();
    public List<CartItems> CartItems { get; set; } = new();
    public List<Favorite> Favorites { get; set; } = new();
    public List<Order> Orders { get; set; } = new();
  
}