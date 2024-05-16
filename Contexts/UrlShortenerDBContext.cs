using Microsoft.EntityFrameworkCore;
using url_shortener.Models;

namespace url_shortener.Contexts;

public class UrlShortenerDBContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    
    public virtual DbSet<ShortUrl> ShortUrls { get; set; }
    
    public UrlShortenerDBContext(DbContextOptions<UrlShortenerDBContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(u => u.ShortUrls)
            .WithOne(u => u.Creator)
            .HasForeignKey(u => u.CreatedBy);

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Login = "admin", Password = "admin", Role = "Admin" },
            new User { Id = 2, Login = "user", Password = "user", Role = "User" }
        );
    }
    
}