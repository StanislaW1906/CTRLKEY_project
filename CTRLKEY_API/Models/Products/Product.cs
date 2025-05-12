using System;
using CTRLKEY_API.Models.Products;
using CTRLKEY_API.Models.Users;
using CTRLKEY_API.Service.Orders;

namespace CTRLKEY_API.Models.Tokens;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
    public int Stock { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string TypeProduct { get; set; } = "Regular"; // скидка, новинка, сол аут, ...
    
}