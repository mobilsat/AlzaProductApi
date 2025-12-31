using AlzaProductApi.Core.Models;

namespace AlzaProductApi.Core.Interfaces;

public interface IProductRepository
{
	Task<IEnumerable<Product>> GetAllAsync();
	Task<Product?> GetByIdAsync(int id);
	Task UpdateAsync(Product product);
	Task SaveChangesAsync();

	// Paging support for v2
	Task<int> CountAsync();
	Task<IEnumerable<Product>> GetPagedAsync(int skip, int take);
}