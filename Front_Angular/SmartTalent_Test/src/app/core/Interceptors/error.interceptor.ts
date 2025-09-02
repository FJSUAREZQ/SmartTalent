import { HttpErrorResponse, HttpInterceptorFn } from "@angular/common/http";
import { catchError, throwError } from "rxjs";


export const errorHandlerInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      let errorMessage = '';

      if (error.error instanceof ErrorEvent) {
        // Cliente-side error, example: network error
        errorMessage = `Error del cliente: ${error.error.message}`;
      } else {
        // Server-side error, example: 404, 500, etc.
        errorMessage = `Server error [${error.status}]: ${error.message}`;

        // Messages according to status code
        switch (error.status) {
          case 400:
            console.warn('Bad Request (400).');
            break;
          case 401:
            console.warn('Unauthorized (401).');
            break;
          case 403:
            console.warn('Forbidden (403).');
            break;
          case 404:
            console.warn('No found (404).');
            break;
          case 500:
            console.warn('Internal Server Error (500).');
            break;
          default:
            console.warn(`Unexpected error: ${error.statusText} (${error.status}).`);
            break;
        }
      }

      // Show message in the console
      console.error(errorMessage);

      // Rethrow the error to the component
      return throwError(() => new Error(errorMessage));
    })
  );
};
