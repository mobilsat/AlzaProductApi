using AlzaProductApi.Core.Dtos;
using AlzaProductApi.Core.Exceptions;
using AlzaProductApi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AlzaProductApi.Web.Controllers.V1;

[ApiController]
[Route("api/v1/products")]
public class ProductsController(IProductService productService) : ControllerBase
{
	/// <summary>
	/// Returns all available products.
	/// </summary>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<IActionResult> GetAll()
	{
		var products = await productService.GetProductsAsync();
		return Ok(products);
	}

	/// <summary>
	/// Returns a single product by its ID.
	/// </summary>
	[HttpGet("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetById(int id)
	{
		var product = await productService.GetProductByIdAsync(id);
		return product is null ? NotFound() : Ok(product);
	}

	/// <summary>
	/// Updates only the description of a product.
	/// </summary>
	[HttpPatch("{id:int}/description")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> UpdateDescription(int id, [FromBody] UpdateProductDescriptionDto dto)
	{
		try
		{
			await productService.UpdateProductDescriptionAsync(id, dto.Description);
			return NoContent();
		}
		catch (ProductNotFoundException)
		{
			return NotFound();
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { error = ex.Message });
		}
	}
}