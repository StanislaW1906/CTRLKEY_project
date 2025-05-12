using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CTRLKEY_API.Data;
using CTRLKEY_API.Models.Tokens;
using CTRLKEY_API.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CTRLKEY_API.Service.Authentication;

public class AuthService
{
    private readonly DBContext _dbContext;
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly EmailService _emailService;
    
    public AuthService(DBContext dbContext,  EmailService emailService)
    {
        _dbContext = dbContext;
        _emailService = emailService; 
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<User> Register( string email, string password)
    {
        //
        if (await _dbContext.Users.AnyAsync(user => user.Email == email))
        {
            return null; 
        }
        //
        var user = new User
        {
            Email = email,
            Role = "User",
        };
        //
        user.PasswordHash = _passwordHasher.HashPassword(user, password);
        //
        _dbContext.Users.Add(user);
        //
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User?> Login(string email, string password)
    {
        //
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
        //
        if (user == null)
        {
            return null;
        }
        //
        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        //
        return result == PasswordVerificationResult.Success ? user : null;
    }

    public async Task CleanupExpiredTokens()
    {
        await _dbContext.ResetTokens.Where(rt => rt.ExpiryDate < DateTime.UtcNow || rt.IsUsed).ExecuteDeleteAsync();
    }
    
    public async Task<string> GenerateResetToken(string email)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            return null;
        }

        // Удаляем старые токены для этого пользователя
        await _dbContext.ResetTokens
            .Where(rt => rt.UserId == user.Id && !rt.IsUsed && rt.ExpiryDate > DateTime.UtcNow)
            .ExecuteDeleteAsync();

        var token = new ResetToken
        {
            Token = Convert.ToHexString(RandomNumberGenerator.GetBytes(32)),
            UserId = user.Id,
            ExpiryDate = DateTime.UtcNow.AddHours(1),
            IsUsed = false
        };

        _dbContext.ResetTokens.Add(token);
        await _dbContext.SaveChangesAsync();
        return token.Token;
    }


    public async Task SendPasswordResetEmail(string email, string resetLink)
    {
        await _emailService.SendPasswordResetEmail(email, resetLink);
    }

    public async Task<bool> ResetPassword(string token, string newPassword)
    {
        var resetToken = await _dbContext.ResetTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => 
                rt.Token == token && 
                !rt.IsUsed && 
                rt.ExpiryDate > DateTime.UtcNow);

        if (resetToken == null)
        {
            return false;
        }

        // Обновляем пароль
        resetToken.User.PasswordHash = _passwordHasher.HashPassword(resetToken.User, newPassword);
        resetToken.IsUsed = true;
    
        await _dbContext.SaveChangesAsync();
        return true;
    }
}