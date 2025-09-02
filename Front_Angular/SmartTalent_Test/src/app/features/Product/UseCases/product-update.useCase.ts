import { Observable } from "rxjs";
import { Inject, Injectable } from "@angular/core";
import { IProductRepository } from "../Services/Repositories/product-repositorio.interface";
import { PRODUCT_REPOSITORY } from "../../../core/Domain/Tokens/Product.repository.token";
import { ProductModel } from "../Models/Product.Model";

@Injectable({
  providedIn: 'root'
})
export class updateProductUseCase {
  
  constructor(@Inject(PRODUCT_REPOSITORY) private repo: IProductRepository) {}

  executeUpdateProduct(produc: ProductModel): Observable<boolean > {
    return this.repo.updateProduct(produc);
  }


}
