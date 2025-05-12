using CTRLKEY_API.Models.Users;

namespace CTRLKEY_API.Models.Tokens;

public class Favorite
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
}