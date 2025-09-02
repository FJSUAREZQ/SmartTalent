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
    public class GetAllProductUseCase : IGetAllProductUseCase
    {
        private readonly IProductRepository _productRepository;


        public GetAllProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        /// <summary>
        /// Get all products with no pagination
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<ProductDTO>> ExecuteAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();

            List<ProductDTO> _productsDto = products.Select(product => new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ModifiedAt = product.ModifiedAt
            }).ToList();

            return _productsDto;
        }



    }
}
