using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class PagedProductResult
    {
        public List<Product> Items { get; set; }       // Productos en esta página
        public int TotalCount { get; set; }            // Total de productos en la base
        public int PageNumber { get; set; }            // Página actual
        public int PageSize { get; set; }              // Tamaño de cada página

        public int TotalPages =>
            PageSize == 0 ? 0 : (int)System.Math.Ceiling((double)TotalCount / PageSize);

        public bool HasNextPage => PageNumber < TotalPages;
        public bool HasPreviousPage => PageNumber > 1;

        public PagedProductResult(List<Product> items, int totalCount, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

    }
}
