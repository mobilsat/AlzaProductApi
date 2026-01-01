namespace AlzaProductApi.Core.Exceptions;

public class ProductNotFoundException(int id) : Exception($"Product with ID {id} was not found.") // primary constructor
{
}
