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
	public async Task V1_PatchDescription_UpdatesDescription_Returns204_AndPersistsChange()
	{
		using var client = new ApiFactory().CreateClient();

		// Arrange
		var newDescription = "New description";

		// Act (PATCH)
		var body = JsonContent.Create(new { description = newDescription });
		var patchRes = await client.PatchAsync("/api/v1/products/1/description", body);

		// Assert (PATCH response)
		patchRes.StatusCode.Should().Be(HttpStatusCode.NoContent);

		// Act (GET to verify persisted change)
		var getRes = await client.GetAsync("/api/v1/products/1");
		getRes.StatusCode.Should().Be(HttpStatusCode.OK);

		var productJson = await getRes.Content.ReadAsStringAsync();
		productJson.Should().Contain(newDescription);
	}
	#endregion

	#region V2
	[Fact]
	public async Task V2_GetProducts_Paged_ReturnsHeaders_WithExpectedValues_AndLinkHeader()
	{
		using var client = new ApiFactory().CreateClient();

		// Act
		var res = await client.GetAsync("/api/v2/products?page=1&pageSize=4");

		// Assert
		res.StatusCode.Should().Be(HttpStatusCode.OK);

		// Headers exist
		res.Headers.Contains("X-Total-Count").Should().BeTrue();
		res.Headers.Contains("X-Page").Should().BeTrue();
		res.Headers.Contains("X-Page-Size").Should().BeTrue();

		// Header values
		res.Headers.GetValues("X-Total-Count").Single().Should().Be("10");
		res.Headers.GetValues("X-Page").Single().Should().Be("1");
		res.Headers.GetValues("X-Page-Size").Single().Should().Be("4");

		// Link header for navigation
		res.Headers.Contains("Link").Should().BeTrue();
		res.Headers.GetValues("Link").Single().Should().Contain("rel=\"first\"");
		res.Headers.GetValues("Link").Single().Should().Contain("rel=\"last\"");
	}
	#endregion
}
