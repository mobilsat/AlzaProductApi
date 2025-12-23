using AlzaProductApi.Core.Interfaces;
using AlzaProductApi.Core.Models;
using AlzaProductApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AlzaProductApi.Infrastructure.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{

	public async Task<IEnumerable<Product>> GetAllAsync() =>
		await context.Products.AsNoTracking().ToListAsync();

	public async Task<Product?> GetByIdAsync(int id) =>
		await context.Products.FindAsync(id);

	public Task UpdateAsync(Product product)
	{
		context.Products.Update(product);
		return Task.CompletedTask;
	}

	public Task SaveChangesAsync() =>
		context.SaveChangesAsync();
}