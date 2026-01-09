using AlzaProductApi.Core.Dtos;
using AlzaProductApi.Core.Models;

namespace AlzaProductApi.Core.Interfaces;


public interface IProductReadFacade
{
	Task<IEnumerable<Product>> GetAllAsync();
	Task<Product?> GetByIdAsync(int id);
	Task<PagedResult<Product>> GetPagedAsync(int page, int pageSize);

	// Cache invalidation (keep minimal & practical)
	void InvalidateAll();
	void InvalidateById(int id);
}
