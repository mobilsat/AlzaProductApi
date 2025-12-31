using AlzaProductApi.Core.Models;
using AlzaProductApi.Core.Dtos;

namespace AlzaProductApi.Core.Interfaces;

public interface IProductService
{
	Task<IEnumerable<Product>> GetProductsAsync();
	Task<Product?> GetProductByIdAsync(int id);
	Task UpdateProductDescriptionAsync(int id, string description);

	// V2 paging
	Task<PagedResult<Product>> GetProductsPagedAsync(int page, int pageSize);

}

