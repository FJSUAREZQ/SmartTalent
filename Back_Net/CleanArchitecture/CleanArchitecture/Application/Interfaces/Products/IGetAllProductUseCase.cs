using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Products
{
    public interface IGetAllProductUseCase
    {
        Task<IEnumerable<ProductDTO>> ExecuteAsync();
    }
}
