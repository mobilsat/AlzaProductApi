using AlzaProductApi.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AlzaProductApi.Tests;

public sealed class ApiFactory : WebApplicationFactory<Program>
{
	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.ConfigureServices(services =>
		{
			// Remove existing repo regstrations
			services.RemoveAll(typeof(IProductRepository));

			// Add mock repo with seed
			services.AddSingleton<IProductRepository>(_ =>
				new InMemoryProductRepository(ProductSeed.Get()));
		});
	}
}
