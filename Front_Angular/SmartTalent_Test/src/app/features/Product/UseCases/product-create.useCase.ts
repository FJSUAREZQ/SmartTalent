import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { PRODUCT_REPOSITORY } from "../../../core/Domain/Tokens/Product.repository.token";
import { ProductModel } from "../Models/Product.Model";
import { IProductRepository } from "../Services/Repositories/product-repositorio.interface";



@Injectable({
  providedIn: 'root'
})
export class createProductUseCase {
  
  constructor(@Inject(PRODUCT_REPOSITORY) private repo: IProductRepository) {}

  executeCreateProduct(product : ProductModel): Observable<number> {
    return this.repo.createProduct(product);
  }

}