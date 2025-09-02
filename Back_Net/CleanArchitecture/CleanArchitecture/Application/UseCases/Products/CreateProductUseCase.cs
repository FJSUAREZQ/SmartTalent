using Application.DTOs;
using Application.Interfaces.Products;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Products
{
    public class CreateProductUseCase : ICreateProductUseCase
    {
        private readonly IProductRepository _productRepository;


        public CreateProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="productDto">Product to create</param>
        /// <returns>Id new Product</returns>
        public async Task<int> ExecuteAsync(ProductDTO productDto)
        {
            Product _productBD = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                CreatedAt = DateTime.UtcNow
            };

            return await _productRepository.CreateProductAsync(_productBD);
        }


    }
}
