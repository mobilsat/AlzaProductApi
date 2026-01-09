using AlzaProductApi.Core.Dtos;
using AlzaProductApi.Core.Interfaces;
using AlzaProductApi.Core.Models;
using Microsoft.Extensions.Caching.Memory;

namespace AlzaProductApi.Core.Services;

public class ProductReadFacade(IProductService service, IMemoryCache cache, IProductCacheKeyRegistry keys)
	: IProductReadFacade
{
	private static readonly MemoryCacheEntryOptions DefaultOptions =
		new() { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) };

	private static string KeyAll => "products:all";
	private static string KeyById(int id) => $"product:{id}";
	private static string KeyPaged(int page, int pageSize) => $"products:p{page}:s{pageSize}";

	public async Task<IEnumerable<Product>> GetAllAsync()
	{
		keys.Track(KeyAll);

		return await cache.GetOrCreateAsync(KeyAll, async entry =>
		{
			entry.SetOptions(DefaultOptions);
			var items = await service.GetProductsAsync();
			return items.ToList();
		}) ?? Enumerable.Empty<Product>();
	}

	public Task<Product?> GetByIdAsync(int id)
	{
		var key = KeyById(id);
		keys.Track(key);

		return cache.GetOrCreateAsync(key, async entry =>
		{
			entry.SetOptions(DefaultOptions);
			return await service.GetProductByIdAsync(id);
		});
	}

	public Task<PagedResult<Product>> GetPagedAsync(int page, int pageSize)
	{
		var key = KeyPaged(page, pageSize);
		keys.Track(key);

		return cache.GetOrCreateAsync(key, async entry =>
		{
			entry.SetOptions(DefaultOptions);
			return await service.GetProductsPagedAsync(page, pageSize);
		})!;
	}

	public void InvalidateAll()
	{
		foreach (var key in keys.Snapshot())
			cache.Remove(key);
	}

	public void InvalidateById(int id)
		=> cache.Remove(KeyById(id));
}