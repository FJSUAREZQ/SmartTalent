import { Component, effect, input, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormGroup, FormControl, Validators} from '@angular/forms';
import { ProductModel } from '../../Models/Product.Model';

@Component({
  selector: 'app-product-component',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './product.component.html',
})
export class ProductFormComponent {
  product: ProductModel = {id: 0,name: '',description: '',price: 0};// Initialize product to avoid undefined issues
  productInComponent = input<ProductModel>(this.product); 
  productOutComponent = output<ProductModel>();
  cancelOutComponent = output<boolean>();

  form = new FormGroup({
    id: new FormControl(0),
    name: new FormControl('', [Validators.required]),
    description: new FormControl(''),
    price: new FormControl(0, [Validators.required, Validators.min(0.01)]),
  });


  constructor() {
    effect(() => {
      const product = this.productInComponent();
      if (product && product.id !== 0) {
        this.loadForm(product);
      }
    });
}


  // Load form with product data
  loadForm(product: ProductModel) {
    if (product) {
      this.form.setValue({
        id: product.id ?? 0,
        name: product.name ?? '',
        description: product.description ?? '',
        price: product.price ?? 0,
      });
    }
  }

  ///Event submit
  onSubmitProductComponent() {
    if (this.form.valid) {
      const value = this.form.value;
      this.product = {
        id: value.id ?? 0,
        name: value.name ?? '',
        description: value.description ?? '',
        price: value.price ?? 0,
      };
      this.productOutComponent.emit(this.product); // Emit the product data
    } else {
      this.form.markAllAsTouched(); //Mark all fields as touched to show validation errors
    }
  }

  ///Event cancel
  onCancelProductComponent() {
    this.cancelOutComponent.emit(true); // Emit cancel event
  }

  ///Data Validators
  get nameInvalid() {
    const control = this.form.get('name');
    return control?.touched && control?.invalid;
  }

  get priceInvalid() {
    const control = this.form.get('price');
    return control?.touched && control?.invalid;
  }
}
