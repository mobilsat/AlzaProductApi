using AlzaProductApi.Core.Models;

public static class ProductSeed
{
	public static IReadOnlyList<Product> Get() => new List<Product>
	{
		new() { Id = 1, Name="Product A", ImgUri="https://example.com/a.jpg", Price=10.99m, Description="Sample product A" },
		new() { Id = 2, Name="Product B", ImgUri="https://example.com/b.jpg", Price=20.50m, Description="Sample product B" },
		new() { Id = 3, Name="Product C", ImgUri="https://example.com/c.jpg", Price=30.00m, Description="Sample product C" },
		new() { Id = 4, Name="Product D", ImgUri="https://example.com/d.jpg", Price=10.99m, Description="Sample product D" },
		new() { Id = 5, Name="Product E", ImgUri="https://example.com/e.jpg", Price=20.50m, Description="Sample product E" },
		new() { Id = 6, Name="Product F", ImgUri="https://example.com/f.jpg", Price=30.00m, Description="Sample product F" },
		new() { Id = 7, Name="Product G", ImgUri="https://example.com/g.jpg", Price=30.00m, Description="Sample product G" },
		new() { Id = 8, Name="Product H", ImgUri="https://example.com/h.jpg", Price=30.00m, Description="Sample product H" },
		new() { Id = 9, Name="Product I", ImgUri="https://example.com/i.jpg", Price=30.00m, Description="Sample product I" },
		new() { Id = 10, Name="Product J", ImgUri="https://example.com/j.jpg", Price=30.00m, Description="Sample product J" },
		// TODO add items
	};
}