namespace AlzaProductApi.Core.Models;

public class Product
{
	public int Id { get; set; }						// required
	public required string Name { get; set; }       // required
	public required string ImgUri { get; set; }     // required
	public decimal Price { get; set; }				// required
	public string? Description { get; set; }		// optional
}
