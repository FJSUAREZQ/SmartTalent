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
    public class DeleteProductUseCase : IDeleteProductUseCase
    {
        private readonly IProductRepository _productRepository;


        public DeleteProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

       
        /// <summary>
        /// Delete a product by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> ExecuteAsync(int id)
        {
            return await _productRepository.DeleteProductAsync(id);
        }


    }
}
