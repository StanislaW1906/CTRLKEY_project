using System.ComponentModel.DataAnnotations;

namespace CTRLKEY_API.Models;

public class ForgotPasswordDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}