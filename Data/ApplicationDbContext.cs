using Microsoft.EntityFrameworkCore;
using UserApi.Models;

namespace UserApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();

    public DbSet<Archive> Archives => Set<Archive>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Archives)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId);
    }
}