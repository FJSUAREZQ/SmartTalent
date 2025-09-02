import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IProductRepository } from "../Services/Repositories/product-repositorio.interface";
import { PRODUCT_REPOSITORY } from "../../../core/Domain/Tokens/Product.repository.token";
import { ProductPagedResultModel } from "../Models/ProductPagedResult.Model";

@Injectable({
  providedIn: 'root'
})
export class getProductsPagedUseCase {
  constructor(@Inject(PRODUCT_REPOSITORY) private repo: IProductRepository) {}

  executeGetProductsPaged(page: number, size: number): Observable<ProductPagedResultModel> {
    return this.repo.getProductsPaged(page, size);
  }


}