import { InjectionToken } from '@angular/core';
import { IProductRepository } from '../../../features/Product/Services/Repositories/product-repositorio.interface';

// token to inject the Product repository interface
export const PRODUCT_REPOSITORY = new InjectionToken<IProductRepository>('IProductRepository');