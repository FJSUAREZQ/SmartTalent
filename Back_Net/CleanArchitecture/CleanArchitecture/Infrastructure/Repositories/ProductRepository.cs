using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.DataContext;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SmartTalentContext _dbContext;

        public ProductRepository(SmartTalentContext context)
        {
            _dbContext = context;
        }

        /// <summary>
        /// Get all products with pagination
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<(IEnumerable<Product> Items, int TotalCount)> GetProductsPagAsync(int pageNumber, int pageSize)
        {
            var query = _dbContext.Products.AsQueryable(); //AsQueryable to build the query dynamically

            var totalCount = await query.CountAsync();// Get total count before pagination

            var items = await query
                .OrderBy(p => p.Id) 
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(); // Execute the query and get the paginated results

            return (items, totalCount);
        }

        /// <summary>
        /// Get all products with no pagination
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await _dbContext.Products.ToListAsync();

            return products;

        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Product> GetProductByIdAsync(int id)
        {
            Product product = _dbContext.Products.Find(id);

            return product;
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<int> CreateProductAsync(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product.Id;
        }

        /// <summary>
        /// Delete a product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteProductAsync(int id)
        {
            Product productDel = await _dbContext.Products.FindAsync(id);

            if (productDel == null)
                return false;

            _dbContext.Products.Remove(productDel);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<bool> UpdateProductAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
