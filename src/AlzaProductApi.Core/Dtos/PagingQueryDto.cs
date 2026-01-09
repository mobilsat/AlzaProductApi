using System.ComponentModel.DataAnnotations;

namespace AlzaProductApi.Core.Dtos;

public class PagingQueryDto
{
	[Range(1, int.MaxValue, ErrorMessage = "page must be >= 1")]
	public int Page { get; init; } = 1;

	[Range(1, 100, ErrorMessage = "pageSize must be between 1 and 100")]
	public int PageSize { get; init; } = 10;
}