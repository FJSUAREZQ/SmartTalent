import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ProductModel } from "../../Models/Product.Model";
import { environment } from "../../../../../environments/environment";
import { ProductPagedResultModel } from "../../Models/ProductPagedResult.Model";


@Injectable({ providedIn: 'root' })
export class ProductApiService {

  private readonly endpoint = environment.apiProductsBaseUrl; 
    
  constructor(private http: HttpClient) {}

  getProductsPaged(pageNumber: number, pageSize: number): Observable<ProductPagedResultModel> {
    return this.http.get<ProductPagedResultModel>(`${this.endpoint}/product/GetAllPag`, {
      params: {
        pageNumber: pageNumber.toString(),
        pageSize: pageSize.toString()
      },
      observe: 'body' // This ensures we only get the body of the response
    });
  }


  getProductById(id: number): Observable<ProductModel> {
    return this.http.get<ProductModel>(`${this.endpoint}/product/GetById/${id}`);
  }


  createProduct(product: ProductModel): Observable<number> {
    return this.http.post<number>(`${this.endpoint}/product/Create`, product);
  }


  updateProduct(product: ProductModel): Observable<boolean> {
    return this.http.put<boolean>(`${this.endpoint}/product/Update/`, product);
  }


  deleteProduct(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.endpoint}/product/Delete/${id}`);
  }
  



}
