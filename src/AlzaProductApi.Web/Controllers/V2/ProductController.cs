using AlzaProductApi.Core.Dtos;
using AlzaProductApi.Core.Interfaces;
using AlzaProductApi.Web.Extensions;
using Asp.Versioning;
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
	public async Task<IActionResult> GetAll([FromQuery] PagingQueryDto q)
	{
		var result = await products.GetPagedAsync(q.Page, q.PageSize);

		Response.AddPaginationHeaders(
			totalCount: result.TotalCount,
			page: result.Page,
			pageSize: result.PageSize,
			buildPageUrl: p => Url.ActionLink(
				nameof(GetAll),
				values: new { version = "2.0", page = p, pageSize = result.PageSize }
			)!
		);

		return Ok(result.Items);
	}
}