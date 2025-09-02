using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Products
{
    public interface IGetProductByIdUseCase
    {
        Task<ProductDTO> ExecuteAsync(int id);
    }
}
