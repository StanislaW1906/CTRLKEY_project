using System.ComponentModel.DataAnnotations;

namespace CTRLKEY_API.Models;

public class ResetPasswordDto
{
    [Required]
    public string Token { get; set; }
    
    [Required]
    [MinLength(6, ErrorMessage = "New password must be at least 6 characters long")]
    public string NewPassword { get; set; }
}