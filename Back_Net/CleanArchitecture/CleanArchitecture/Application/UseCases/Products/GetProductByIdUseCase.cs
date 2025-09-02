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
    public class GetProductByIdUseCase : IGetProductByIdUseCase
    {
        private readonly IProductRepository _productRepository;


        public GetProductByIdUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        /// <summary>
        /// Get product by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ProductDTO> ExecuteAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            ProductDTO _productDTO = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ModifiedAt = product.ModifiedAt
            };

            return _productDTO;
        }


        
    }
}
