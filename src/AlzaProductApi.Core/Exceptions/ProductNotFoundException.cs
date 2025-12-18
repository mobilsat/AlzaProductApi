using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaProductApi.Core.Exceptions;

public class ProductNotFoundException(int id) : Exception($"Product with ID {id} was not found.") // primary constructor
{
}
