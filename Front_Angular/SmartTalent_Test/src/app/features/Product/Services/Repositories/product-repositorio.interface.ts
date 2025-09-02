import { Observable } from "rxjs";
import { ProductModel  } from "../../Models/Product.Model";
import { ProductPagedResultModel } from "../../Models/ProductPagedResult.Model";



export interface IProductRepository {
  getProductsPaged(pageNumber: number, pageSize: number): Observable<ProductPagedResultModel>;
  getProductById(id: number): Observable<ProductModel>;
  createProduct(product: ProductModel): Observable<number>;
  updateProduct(product: ProductModel): Observable<boolean>;
  deleteProduct(id: number): Observable<boolean>;
}
