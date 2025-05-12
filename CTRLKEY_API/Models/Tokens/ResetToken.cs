using System;
using CTRLKEY_API.Models.Users;

namespace CTRLKEY_API.Models.Tokens;

public class ResetToken
{
    public int Id { get; set; }
    public string Token { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsUsed { get; set; }
    
}