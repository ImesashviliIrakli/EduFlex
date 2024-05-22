using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Data;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id);

            // Configure the relationship
            entity.HasOne<IdentityUser>()
                  .WithOne()
                  .HasForeignKey<Student>(e => e.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Ensure PrivateNumber is unique
            entity.HasIndex(e => e.PrivateNumber).IsUnique();
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Id);

            // Configure the relationship
            entity.HasOne<IdentityUser>()
                  .WithOne()
                  .HasForeignKey<Teacher>(e => e.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Ensure PrivateNumber is unique
            entity.HasIndex(e => e.PrivateNumber).IsUnique();
        });
    }
}
