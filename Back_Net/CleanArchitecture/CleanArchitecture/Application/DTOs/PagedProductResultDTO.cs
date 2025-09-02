using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    /// <summary>
    /// DTO for paginated product results and calculating values like total pages, next/previous page
    /// </summary>
    public class PagedProductResultDTO
    {
        public IEnumerable<ProductDTO> Items { get; set; }  // Products in the current page
        public int TotalCount { get; set; }    // Total number of products
        public int PageNumber { get; set; }    // Number of the current page
        public int PageSize { get; set; }      // Size of each page
        public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling((double)TotalCount / PageSize); // Calculate total pages
        public bool HasNextPage => PageNumber < TotalPages; //Indicates if there is a next page
        public bool HasPreviousPage => PageNumber > 1; // Indicates if there is a previous page

    }
}
