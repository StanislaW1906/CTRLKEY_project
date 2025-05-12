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
    public async Task<ActionResult> Register([FromForm] string email, [FromForm] string password)
    {
        var user = await _authService.Register(email, password);
        if (user == null)
        {
            return BadRequest("User already exists");
        }
        
        return Ok(user);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromForm] string email, [FromForm] string password)
    {
        var user = await _authService.Login(email, password);
        if (user == null)
        {
            return BadRequest("User not found");
        }
        return Ok(user);
    }
}
