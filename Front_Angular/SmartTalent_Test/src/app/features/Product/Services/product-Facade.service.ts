import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ProductModel } from "../Models/Product.Model";
import { ProductPagedResultModel } from "../Models/ProductPagedResult.Model";
import { createProductUseCase } from "../UseCases/product-create.useCase";
import { deleteProductUseCase } from "../UseCases/product-delete.useCase";
import { getProductByIdUseCase } from "../UseCases/product-getById.useCase";
import { getProductsPagedUseCase } from "../UseCases/product-getPaged.useCase";
import { updateProductUseCase } from "../UseCases/product-update.useCase";




@Injectable({
  providedIn: 'root'
})
export class ProductFacade {

  // inject all use cases
  constructor(
    private createProductUC: createProductUseCase,
    private deleteProductUC: deleteProductUseCase,
    private updateProductUC: updateProductUseCase,
    private getProductByIdUC: getProductByIdUseCase,
    private getAllProductsPagedUC: getProductsPagedUseCase
  ) {}


  createProduct(product: ProductModel): Observable<number> {
    return this.createProductUC.executeCreateProduct(product);
  }


  deleteProduct(id: number): Observable<boolean> {
    return this.deleteProductUC.executeDeleteProduct(id);
  }


  updateProduct(product: ProductModel): Observable<boolean> {
    return this.updateProductUC.executeUpdateProduct(product);
  }


  getProductsById(id: number): Observable<ProductModel> {
    return this.getProductByIdUC.excecuteGetProductsById(id);
  }


  getAllProductsPaged(page: number, size: number): Observable<ProductPagedResultModel> {
    return this.getAllProductsPagedUC.executeGetProductsPaged(page, size);
  }


}