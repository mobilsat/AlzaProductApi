using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace AlzaProductApi.Tests;

public class ProductsControllerTests
{
	
	#region V1
	[Fact]
	public async Task V1_GetProducts_Returns200AndList()
	{
		using var factory = new ApiFactory();
		using var client = factory.CreateClient();

		var res = await client.GetAsync("/api/v1/products");
		res.StatusCode.Should().Be(HttpStatusCode.OK);

		var json = await res.Content.ReadAsStringAsync();
		json.Should().Contain("Product A");
	}

	[Fact]
	public async Task V1_GetById_Unknown_Returns404()
	{
		using var client = new ApiFactory().CreateClient();
		var res = await client.GetAsync("/api/v1/products/9999");
		res.StatusCode.Should().Be(HttpStatusCode.NotFound);
	}

	[Fact]
	public async Task V1_PatchDescription_UpdatesDescription_Returns204()
	{
		using var client = new ApiFactory().CreateClient();

		var body = JsonContent.Create(new { description = "New description" });
		var res = await client.PatchAsync("/api/v1/products/1/description", body);

		res.StatusCode.Should().Be(HttpStatusCode.NoContent);
	}
	#endregion

	#region V2
	[Fact]
	public async Task V2_GetProducts_Paged_ReturnsHeaders()
	{
		using var client = new ApiFactory().CreateClient();

		var res = await client.GetAsync("/api/v2/products?page=1&pageSize=2");
		res.StatusCode.Should().Be(HttpStatusCode.OK);

		res.Headers.Contains("X-Total-Count").Should().BeTrue();
		res.Headers.Contains("X-Page").Should().BeTrue();
		res.Headers.Contains("X-Page-Size").Should().BeTrue();
	}
	#endregion
}


