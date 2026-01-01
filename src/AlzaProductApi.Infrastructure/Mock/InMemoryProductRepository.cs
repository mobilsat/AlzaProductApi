using AlzaProductApi.Core.Interfaces;
using AlzaProductApi.Core.Models;

public sealed class InMemoryProductRepository : IProductRepository
{
	private readonly List<Product> _products;

	public InMemoryProductRepository(IEnumerable<Product> seed)
		=> _products = seed.ToList();

	public Task<IEnumerable<Product>> GetAllAsync() 
		=> Task.FromResult<IEnumerable<Product>>(_products.ToList());

	public Task<Product?> GetByIdAsync(int id)
		=> Task.FromResult(_products.SingleOrDefault(p => p.Id == id));

	public Task AddAsync(Product product) { _products.Add(product); return Task.CompletedTask; }

	public Task UpdateAsync(Product product) => Task.CompletedTask;

	public Task<int> CountAsync() => Task.FromResult(_products.Count);

	public Task<IEnumerable<Product>> GetPagedAsync(int skip, int take)
		=> Task.FromResult<IEnumerable<Product>>(_products.Skip(skip).Take(take).ToList());

	public Task SaveChangesAsync() => Task.CompletedTask;
}