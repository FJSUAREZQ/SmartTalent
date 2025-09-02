using Application.DTOs;

namespace WebAPI.DTOs
{
    public class PagedProductResultApiDTO
    {
        public IEnumerable<ProductApiDTO> Items { get; set; }       // Products in the current page
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

    }
}
