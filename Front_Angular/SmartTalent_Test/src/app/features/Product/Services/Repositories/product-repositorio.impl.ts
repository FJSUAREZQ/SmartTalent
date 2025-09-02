import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { IProductRepository } from "./product-repositorio.interface";
import { ProductPagedResultModel } from "../../Models/ProductPagedResult.Model";
import { ProductModel } from "../../Models/Product.Model";
import { ProductApiService } from "../API/product-api.service";

@Injectable()
export class ProductRepositoryImpl implements IProductRepository {
  
  constructor(private api: ProductApiService) {}

  getProductsPaged(pageNumber: number, pageSize: number): Observable<ProductPagedResultModel> {
    return this.api.getProductsPaged(pageNumber, pageSize);
  }


  getProductById(id: number): Observable<ProductModel> {
    return this.api.getProductById(id);
  }


  createProduct(product: ProductModel): Observable<number> {
    return this.api.createProduct(product);
  }


  updateProduct(product: ProductModel): Observable<boolean> {
    return this.api.updateProduct(product);
  }

  
  deleteProduct(id: number): Observable<boolean> {
    return this.api.deleteProduct(id);
  }

    

  
}
