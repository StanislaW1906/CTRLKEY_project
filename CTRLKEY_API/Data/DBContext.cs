using CTRLKEY_API.Models.Orders;
using CTRLKEY_API.Models.Products;
using CTRLKEY_API.Models.Tokens;
using CTRLKEY_API.Models.Users;
using CTRLKEY_API.Service.Orders;
using CTRLKEY_API.Service.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CTRLKEY_API.Data;

public class DBContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<CartItems> CartItems { get; set; }
    public DbSet<OrderItems> OrderItems { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ResetToken> ResetTokens { get; set; }

    public DBContext(DbContextOptions<DBContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        
     
        
        builder.Entity<ResetToken>().HasIndex(rt => rt.Token).IsUnique();
        builder.Entity<ResetToken>().HasIndex(
            rt => new {
                rt.UserId,
                rt.IsUsed,
                rt.ExpiryDate
            });

        builder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Email = "admin",
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "admin"),
                Role = "Admin"
            });
        builder.Entity<User>().HasData(
            new User
            {
                Id = 5,
                Email = "seller@gmail.com",
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "seller"),
                Role = "Seller"
            });
    }

}