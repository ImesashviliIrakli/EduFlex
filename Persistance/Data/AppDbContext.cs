using Domain;
using Identity.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Data;

public class AppDBContext : DbContext
{
	public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
	public DbSet<Student> Students { get; set; }
	public DbSet<Teacher> Teachers { get; set; }
	public DbSet<Course> Courses { get; set; }
	public DbSet<Faculty> Faculties { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Student>(entity =>
		{
			entity.HasKey(e => e.Id);

			entity.HasIndex(e => e.PrivateNumber).IsUnique();
			entity.HasIndex(e => e.UserId).IsUnique();
		});

		modelBuilder.Entity<Teacher>(entity =>
		{
			entity.HasKey(e => e.Id);

			entity.HasIndex(e => e.PrivateNumber).IsUnique();
			entity.HasIndex(e => e.UserId).IsUnique();
		});
	}
}
