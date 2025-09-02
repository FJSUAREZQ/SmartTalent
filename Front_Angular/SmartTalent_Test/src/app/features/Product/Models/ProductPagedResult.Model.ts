import { ProductModel } from "./Product.Model";

export interface ProductPagedResultModel {
  items: ProductModel[];     // List of products in the current page
  totalCount: number;   // total records available in the database
  pageNumber: number;    // Current page index
  pageSize: number;     // Page size (number of items per page)
  totalPages: number;   // Total of pages available
  hasNextPage: boolean; // If there is a next page
  hasPreviousPage: boolean; // if there is a previous page

}