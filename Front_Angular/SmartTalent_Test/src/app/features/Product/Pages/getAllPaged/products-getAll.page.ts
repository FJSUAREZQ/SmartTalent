import { ChangeDetectorRef, Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ProductFacade } from '../../Services/product-Facade.service';
import { ProductModel } from '../../Models/Product.Model';
import { Router } from '@angular/router';
import { environment } from '../../../../../environments/environment';

@Component({
  selector: 'app-products-getAll',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './products-getAll.page.html',
})
export class ProductsGetAllPage {
  private facade = inject(ProductFacade);
  //private cdr = inject(ChangeDetectorRef); // Inject ChangeDetectorRef for manual change detection

  // Signals to hold state
  products = signal<ProductModel[] | null>([]); //
  pageNumber = signal<number>(1);
  pageSize = signal<number>(environment.defaultPageSize);
  totalCount = signal<number>(0);
  totalPages = signal<number>(0);
  hasNextPage = signal<boolean>(false);
  hasPreviousPage = signal<boolean>(false);

  isLoading = signal<boolean>(true);
  isDeleted = signal<boolean>(false);
  isUpdated = signal<boolean>(false);
  error = signal<string | null>(null);


  constructor(private router: Router) {
    this.loadProducts(this.pageNumber(), this.pageSize());
  }

  /// Load products with pagination
  loadProducts(pageNumber: number, pageSize: number) {
    // Fetch paged products on initialization
    this.facade.getAllProductsPaged(pageNumber, pageSize).subscribe((result) => {
      // Update products signal
      this.products.set(result.items);
      // Update pagination signals
      this.pageNumber.set(result.pageNumber);
      this.pageSize.set(result.pageSize);
      this.totalCount.set(result.totalCount);
      this.totalPages.set(result.totalPages);
      this.hasNextPage.set(result.hasNextPage);
      this.hasPreviousPage.set(result.hasPreviousPage);
      // Update loading and error states
      this.isLoading.set(false);
      this.error.set(null);
    });
  }
      
/// Event handler for updating a product
updateProduct(productId: number) {
  this.router.navigate(['/UpDate', productId]);
}

/// Event handler for deleting a product  
deleteProduct(productId: number, name:string) {
 var confirmed = confirm(`Do you want to delete : ${name}?`);

  if (!confirmed) {
    return; // if user cancels, exit the function
  }

  this.facade.deleteProduct(productId).subscribe((deleted) => {
    this.isDeleted.set(deleted);

    if (this.isDeleted()) {
      alert('Product deleted successfully.');
      this.pageNumber.set(1); // Reset to the first page after deletion
      this.loadProducts(this.pageNumber(), this.pageSize());
    } else {
      alert('Error deleting the product.');
    }
    this.isDeleted.set(false); // Reset the deletion state after handling
  });
}

/// *** PAGINATION METHODS ***
///Event handler for going to the previous page in the pagination
onPrevious(): void {
  if (this.hasPreviousPage()) {
    this.pageNumber.set(this.pageNumber() - 1);
    this.loadProducts(this.pageNumber(), this.pageSize());
  }
}

/// Event handler for going to the next page in the pagination
onNext(): void {
  if (this.hasNextPage()) {
    this.pageNumber.set(this.pageNumber() + 1);
    this.loadProducts(this.pageNumber(), this.pageSize());
  }
}

///Event handler for selecting a specific page in the pagination
onPageSelect(page: number): void {
  this.pageNumber.set(page);
  this.loadProducts(this.pageNumber(), this.pageSize());
}



}
