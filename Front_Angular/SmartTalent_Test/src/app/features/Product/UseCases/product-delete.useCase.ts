import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { PRODUCT_REPOSITORY } from "../../../core/Domain/Tokens/Product.repository.token";
import { IProductRepository } from "../Services/Repositories/product-repositorio.interface";

@Injectable({
  providedIn: 'root'
})
export class deleteProductUseCase {
  
  constructor(@Inject(PRODUCT_REPOSITORY) private repo: IProductRepository) {}

  executeDeleteProduct(productId:number): Observable<boolean> {
    return this.repo.deleteProduct(productId);
  }

}