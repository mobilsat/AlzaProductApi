using AlzaProductApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaProductApi.Core.Interfaces;

public interface IProductService
{
	Task<IEnumerable<Product>> GetProductsAsync();
	Task<Product?> GetProductByIdAsync(int id);
	Task UpdateProductDescriptionAsync(int id, string description);
}

