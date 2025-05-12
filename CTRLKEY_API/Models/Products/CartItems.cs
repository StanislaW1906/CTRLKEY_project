using CTRLKEY_API.Models.Tokens;
using CTRLKEY_API.Models.Users;

namespace CTRLKEY_API.Models.Products;

public class CartItems
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}