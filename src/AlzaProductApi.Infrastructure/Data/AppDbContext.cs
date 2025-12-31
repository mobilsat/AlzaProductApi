using Microsoft.EntityFrameworkCore;
using AlzaProductApi.Core.Models;

namespace AlzaProductApi.Infrastructure.Data;

public class AppDbContext : DbContext
{
	public DbSet<Product> Products => Set<Product>();

	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Product>()
			.Property(p => p.Price)
			.HasPrecision(18, 2);


		// Seed data
		modelBuilder.Entity<Product>().HasData(
			new Product { Id = 1, Name = "Product A", ImgUri = "https://example.com/a.jpg", Price = 10.99m, Description = "Sample product A" },
			new Product { Id = 2, Name = "Product B", ImgUri = "https://example.com/b.jpg", Price = 20.50m, Description = "Sample product B" },
			new Product { Id = 3, Name = "Product C", ImgUri = "https://example.com/c.jpg", Price = 30.00m, Description = "Sample product C" }
		);
	}
}
