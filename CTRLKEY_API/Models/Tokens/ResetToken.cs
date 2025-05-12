using CTRLKEY_project.Models.Users;
namespace CTRLKEY_project.Models.Tokens;

public class ResetToken
{
    public int Id { get; set; }
    public string Token { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsUsed { get; set; }
    
}