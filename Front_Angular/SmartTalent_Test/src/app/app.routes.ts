import { Routes } from '@angular/router';


export const routes: Routes = [
    {
        path: '',
        redirectTo: 'List',
        pathMatch: 'full'
    },
    {
        path: 'List', async loadComponent() {
            const c = await import('./features/Product/Pages/getAllPaged/products-getAll.page');
            return c.ProductsGetAllPage;
        }
    },
    {
        path: 'Create', async loadComponent() {
            const c = await import('./features/Product/Pages/create/product-Create.page');
            return c.ProductCreatePage;
        }
    },
    {
        path: 'UpDate/:productId', async loadComponent() {
            const c = await import('./features/Product/Pages/upDate/product-UpDate.page');
            return c.ProductUpDate;
        }   
    }   
];
