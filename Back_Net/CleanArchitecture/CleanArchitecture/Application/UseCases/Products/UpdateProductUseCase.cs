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
    public class UpdateProductUseCase : IUpdateProductUseCase
    {
        private readonly IProductRepository _productRepository;


        public UpdateProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        /// <summary>
        /// Update an existing product
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> ExecuteAsync(ProductDTO productDto)
        {
            var productExist = await _productRepository.GetProductByIdAsync(productDto.Id);

            if (productExist == null)
                return false;


            productExist.Name = productDto.Name;
            productExist.Description = productDto.Description;
            productExist.Price = productDto.Price;
            productExist.ModifiedAt = DateTime.UtcNow;

            return await _productRepository.UpdateProductAsync(productExist);
        }



    }
}
