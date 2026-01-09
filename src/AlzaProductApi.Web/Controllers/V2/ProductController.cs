using Asp.Versioning;
using AlzaProductApi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AlzaProductApi.Web.Controllers.V2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/products")]
public class ProductsController(IProductReadFacade products) : ControllerBase
{
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
	{
		if (page < 1 || pageSize < 1)
			return BadRequest(new { error = "page and pageSize must be >= 1" });

		var result = await products.GetPagedAsync(page, pageSize);

		Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
		Response.Headers["X-Page"] = result.Page.ToString();
		Response.Headers["X-Page-Size"] = result.PageSize.ToString();

		var lastPage = (int)Math.Ceiling(result.TotalCount / (double)result.PageSize);
		if (lastPage < 1) lastPage = 1;

		var links = new List<string>();
		string BaseUrl(int p) => Url.ActionLink(nameof(GetAll), values: new { version = "2.0", page = p, pageSize = result.PageSize })!;

		if (result.Page > 1)
			links.Add($"<{BaseUrl(result.Page - 1)}>; rel=\"prev\"");
		if (result.Page < lastPage)
			links.Add($"<{BaseUrl(result.Page + 1)}>; rel=\"next\"");

		links.Add($"<{BaseUrl(1)}>; rel=\"first\"");
		links.Add($"<{BaseUrl(lastPage)}>; rel=\"last\"");

		Response.Headers["Link"] = string.Join(", ", links);

		return Ok(result.Items);
	}
}