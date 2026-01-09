using System.Collections.Concurrent;

namespace AlzaProductApi.Core.Services;

public interface IProductCacheKeyRegistry
{
	void Track(string key);
	IEnumerable<string> Snapshot();
}

public class ProductCacheKeyRegistry : IProductCacheKeyRegistry
{
	private readonly ConcurrentDictionary<string, byte> _keys = new();

	public void Track(string key) => _keys.TryAdd(key, 0);

	public IEnumerable<string> Snapshot() => _keys.Keys;
}