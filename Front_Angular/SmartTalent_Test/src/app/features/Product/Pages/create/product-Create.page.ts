import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductFacade } from '../../Services/product-Facade.service';
import { ProductFormComponent } from '../../Components/Product/product.component';
import { ProductModel } from '../../Models/Product.Model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-create',
  standalone: true,
  imports: [CommonModule, ProductFormComponent],
  templateUrl: './product-Create.page.html',
})


export class ProductCreatePage {
  private facade = inject(ProductFacade);
  private idResponse= signal<number>(0);

  //Signals to interact with child component
  productToChild = signal<ProductModel>({id: 0,name: '',description: '',price: 0});
  cancelFromChild = signal<boolean>(false);


  constructor(private router: Router) {  }

  // Method to save product using facade
saveProduct(product: ProductModel) {
    this.facade.createProduct(product).subscribe((id: number) => {
      this.idResponse.set(id);

      if (id === 0) {
        alert('Error creating product');
        return;
      }else{
        alert('Product created with ID: ' + id);
        this.router.navigate(['/List']);
      }
      this.idResponse.set(0);
    });
  }

///Event emitter from child component to save product
  handleSave(product: ProductModel) {
    this.productToChild.set(product);
    this.saveProduct(product);
  }

  /// Cancel action from child component
  handleCancel(e :boolean) {
    this.cancelFromChild.set(e);

    if (this.cancelFromChild()) {
       this.router.navigate(['/List']);
    }
  }



}
