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
    public class GetProductPagedUseCase : IGetProductPagedUseCase
    {
        private readonly IProductRepository _productRepository;


        public GetProductPagedUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        /// <summary>
        /// Get products with pagination
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedProductResultDTO> ExecuteAsync(int pageNumber, int pageSize)
        {
            var (items, totalCount) = await _productRepository.GetProductsPagAsync(pageNumber, pageSize);

            PagedProductResultDTO result = new PagedProductResultDTO
            {
                Items = items.Select(product => new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ModifiedAt = product.ModifiedAt
                }).ToList(),
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return result;
        }


        
    }
}
