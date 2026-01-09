using System.ComponentModel.DataAnnotations;

namespace AlzaProductApi.Core.Dtos;

public class UpdateProductDescriptionDto
{
	[Required]
	[MinLength(1)]
	public string Description { get; set; } = string.Empty;
}