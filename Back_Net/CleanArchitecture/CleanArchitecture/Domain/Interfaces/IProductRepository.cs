using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        //Returns a paginated list of products with the total count of products
        Task<(IEnumerable<Product> Items, int TotalCount)> GetProductsPagAsync(int pageNumber, int pageSize);

        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product> GetProductByIdAsync(int id);

        Task<int> CreateProductAsync(Product product);
        
        Task<bool> UpdateProductAsync(Product product);
        
        Task<bool> DeleteProductAsync(int id);


    }
}
