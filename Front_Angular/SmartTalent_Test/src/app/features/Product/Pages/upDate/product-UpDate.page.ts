import { Component, inject, OnInit, signal, Signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductModel } from '../../Models/Product.Model';
import { ProductFacade } from '../../Services/product-Facade.service';
import { ProductFormComponent } from '../../Components/Product/product.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-UpDate',
  templateUrl: 'product-UpDate.page.html',
  standalone: true,
  imports: [CommonModule, ProductFormComponent],
})


export class ProductUpDate implements OnInit {
  private facade = inject(ProductFacade);
  productId = signal<number>(0); //Value received from the route
  isLoading = signal<boolean>(true);
  isUpdated = signal<boolean>(false);

  //Signals to interact with child component
  productToChild = signal<ProductModel>({id: 0,name: '',description: '',price: 0});
  cancelFromChild = signal<boolean>(false);

  constructor(private route: ActivatedRoute, private router: Router) {
    this.productId.set(Number(this.route.snapshot.paramMap.get('productId')));
    this.loadProductById(this.productId());
  }

  ngOnInit() {}


  // Method to update product using facade
  loadProductById(id: number) {
    this.facade.getProductsById(id).subscribe((result) => {
      this.productToChild.set(result);
      this.isLoading.set(false);
    });
  }

upDateProduct(product: ProductModel) {
    this.facade.updateProduct(product).subscribe((result) => {
        this.isUpdated.set(result);

        if (this.isUpdated()) {
          alert('Product updated successfully');
          this.router.navigate(['/List']);
        } else {
          alert('Error updating product');
        }   
        this.isUpdated.set(false);
    });
  }


  ///Event emitter from child component to save product
  handleUpdate(product: ProductModel) {
    this.productToChild.set(product);
    this.upDateProduct(product);
  }

  /// Cancel action from child component
  handleCancel(e :boolean) {
    this.cancelFromChild.set(e);

    if (this.cancelFromChild()) {
       this.router.navigate(['/List']);
    }
  }


}
