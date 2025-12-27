using AlzaProductApi.Core.Exceptions;
using AlzaProductApi.Core.Interfaces;
using AlzaProductApi.Core.Models;

namespace AlzaProductApi.Core.Services;

public class ProductService(IProductRepository repository) : IProductService
{
	public Task<IEnumerable<Product>> GetProductsAsync()
		=> repository.GetAllAsync();

	public Task<Product?> GetProductByIdAsync(int id)
		=> repository.GetByIdAsync(id);

	public async Task UpdateProductDescriptionAsync(int id, string description)
	{
		if (string.IsNullOrWhiteSpace(description))
			throw new ArgumentException("Description must not be empty.", nameof(description));

		var product = await repository.GetByIdAsync(id);
		if (product is null)
			throw new ProductNotFoundException(id);

		product.Description = description.Trim();

		await repository.UpdateAsync(product);
		await repository.SaveChangesAsync();
	}
}