using CTRLKEY_project.Models.Tokens;
using CTRLKEY_project.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace CTRLKEY_project.Data;

public class DBContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<ResetToken> ResetTokens { get; set; }
}