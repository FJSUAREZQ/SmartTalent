import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { errorHandlerInterceptor } from './core/Interceptors/error.interceptor';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { PRODUCT_REPOSITORY } from './core/Domain/Tokens/Product.repository.token';
import { ProductRepositoryImpl } from './features/Product/Services/Repositories/product-repositorio.impl';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),

    
    provideHttpClient(withFetch(), withInterceptors([errorHandlerInterceptor])),

    //to inject the repository implementation
    //to identify the repository implementation
    { provide: PRODUCT_REPOSITORY, useClass: ProductRepositoryImpl }
  ]
};
