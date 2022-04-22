import { Injectable } from '@angular/core';
import { HttpEvent, HttpRequest, HttpHandler, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { ApiService } from '../api/services';
import { MatSnackBar } from '@angular/material/snack-bar';


@Injectable()
export class ServerErrorInterceptor implements HttpInterceptor {

  constructor(private apiService : ApiService, public snackBar: MatSnackBar) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return next.handle(request).pipe(
      retry(0),
      catchError((error: HttpErrorResponse) => {
        const msg = (error.error) ? 'Error: ' + error.status + ' message: ' + (error.error.message ? error.error.message : error.statusText) : error.message;
        const configClose: any = {
          duration: 5000,
          verticalPosition: 'top',
          horizontalPosition: 'right'
        };
        switch (error.status) {
          case 400:
            this.snackBar.open(msg, 'Close', configClose);
            break;
          case 401:
            this.snackBar.open(msg, 'Close', configClose);
            break;
          case 403:
            this.snackBar.open(msg, 'Close', configClose);
            break;
          case 404:
            this.snackBar.open(msg, 'Close', configClose);
            break;
          default:
            this.snackBar.open(msg, 'Close', configClose);
            console.error('An error occurred while processing your operation');
        }

        return throwError(error);
        })

    );
  }
}
