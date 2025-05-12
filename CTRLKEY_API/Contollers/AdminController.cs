using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CTRLKEY_API.Data;
namespace CTRLKEY_API.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly DBContext _database;

    public AdminController(DBContext database)
    {
        _database = database;
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _database.Users.Select(u => new { u.Id, u.Email, u.Role }).ToListAsync();
        return Ok(users);
    }

    [HttpDelete("users/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _database.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        
        _database.Users.Remove(user);
        await _database.SaveChangesAsync();
        return Ok();
    }
}