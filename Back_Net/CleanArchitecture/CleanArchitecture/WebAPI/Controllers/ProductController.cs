using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ICreateProductUseCase _createProductUseCase;
        private readonly IDeleteProductUseCase _deleteProductUseCase;
        private readonly IGetAllProductUseCase _getAllProductUseCase;
        private readonly IGetProductByIdUseCase _getProductByIdUseCase;
        private readonly IGetProductPagedUseCase _getProductPagedUseCase;
        private readonly IUpdateProductUseCase _updateProductUseCase;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productoService"></param>
        public ProductController(IDeleteProductUseCase deleteProductUseCase,
                                 IGetAllProductUseCase getAllProductUseCase,
                                 IGetProductByIdUseCase getProductByIdUseCase,
                                 IGetProductPagedUseCase getProductPagedUseCase,
                                 ICreateProductUseCase createProductUseCase,
                                 IUpdateProductUseCase updateProductUseCase)
        {
            _deleteProductUseCase = deleteProductUseCase;
            _getAllProductUseCase = getAllProductUseCase;
            _getProductByIdUseCase = getProductByIdUseCase;
            _getProductPagedUseCase = getProductPagedUseCase;
            _createProductUseCase = createProductUseCase;
            _updateProductUseCase = updateProductUseCase;
        }

        //api/product/GetAllPag?pageNumber=2&pageSize=3
        [HttpGet("GetAllPag")]
        public async Task<IActionResult> GetAllProductsPag(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var productosService = await _getProductPagedUseCase.ExecuteAsync(pageNumber, pageSize);

                if (productosService == null || !productosService.Items.Any())
                {
                    return NotFound("There are not products");
                }

                var response = new PagedProductResultApiDTO
                {
                    Items = productosService.Items.Select(p => new ProductApiDTO
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        ModifiedAt = p.ModifiedAt
                    }).ToList(),
                    TotalCount = productosService.TotalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = productosService.TotalPages,
                    HasNextPage = productosService.HasNextPage,
                    HasPreviousPage = productosService.HasPreviousPage
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal server error", detail = ex.Message });
            }
        }

        /// <summary>
        /// Get all products without pagination
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProducts()
        {
            try 
            {
                var productosService = await _getAllProductUseCase.ExecuteAsync();

                if (productosService == null || !productosService.Any())
                {
                    return NotFound("There are not products");
                }

                var response = productosService.Select(p => new ProductApiDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ModifiedAt = p.ModifiedAt
                }).ToList();

                return Ok(response);
            }
            catch(Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal server error", detail = ex.Message });
            }
        }

        /// <summary>
        /// Get a product by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id) 
        {
            try 
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var productService = await _getProductByIdUseCase.ExecuteAsync(id);

                if (productService == null)
                {
                    return NotFound("There are not products");
                }

                ProductApiDTO product = new ProductApiDTO
                {
                    Id = productService.Id,
                    Name = productService.Name,
                    Description = productService.Description,
                    Price = productService.Price,
                    ModifiedAt = productService.ModifiedAt
                };

                return Ok(product);
            } 
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal server error", detail = ex.Message });
            }

        }


        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ProductDTO product)
        {
            try 
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (product == null)
                {
                    return NotFound("collection is empty");
                }

                int response = await _createProductUseCase.ExecuteAsync(product); //retuns the new product Id

                if (response <= 0)
                {
                    return NoContent();
                }

                return CreatedAtAction( nameof(GetById),        // to provide the location of the new created resource
                                        new { id = response }, // route values
                                        response);            // response body (new created product Id)
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal server error", detail = ex.Message });
            }
        }


        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ProductDTO product)
        {
            try 
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool resp = await _updateProductUseCase.ExecuteAsync(product);

                if (!resp)
                {
                    return NoContent();
                }

                return Ok(resp);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal server error", detail = ex.Message });
            }
        }


        /// <summary>
        /// Delete a product by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool resp = await _deleteProductUseCase.ExecuteAsync(id);

                if (!resp)
                {
                    return NoContent();
                }

                return Ok(resp);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal server error", detail = ex.Message });
            }
        }



    }
}
