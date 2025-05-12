using System.Threading.Tasks;
using CTRLKEY_API.Models;
using CTRLKEY_API.Models.DTOs;
using CTRLKEY_API.Models.Users;
using CTRLKEY_API.Service.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CTRLKEY_API.Contollers.Authentication;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterDto dto)
    {
        var user = await _authService.Register(dto.Email, dto.Password);
        if (user == null)
        {
            return BadRequest("User already exists");
        }
        
        return Ok(user);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _authService.Login(dto.Email, dto.Password);
        if (user == null)
        {
            return BadRequest("User not found");
        }
        return Ok(user);
    }
    
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
    {
        var resetToken = await _authService.GenerateResetToken(dto.Email);
        if (resetToken == null) return NotFound("User not found");

        var resetLink = $"https://localhost:44353/api/auth/reset-password?token={resetToken}";
        await _authService.SendPasswordResetEmail(dto.Email, resetLink);
        return Ok();
    }
    
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
    {
        var result = await _authService.ResetPassword(dto.Token, dto.NewPassword);
        return result ? Ok() : BadRequest("Invalid or expired token");
    }
}
