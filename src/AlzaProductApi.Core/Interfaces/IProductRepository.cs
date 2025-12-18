using AlzaProductApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaProductApi.Core.Interfaces;

public interface IProductRepository
{
	Task<IEnumerable<Product>> GetAllAsync();
	Task<Product?> GetByIdAsync(int id);
	Task UpdateAsync(Product product);
	Task SaveChangesAsync();
}