import { NavigationExtras, Router } from '@angular/router';
import { HttpClient, HttpErrorResponse, HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { ApiService } from '../api/services';

@Injectable({
  providedIn: 'root'
})
export class ServerHeaderInterceptor implements HttpInterceptor  {

  constructor(private apiService : ApiService, public http: HttpClient, public router: Router) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {


    let headers = new HttpHeaders()
      .set('Authorization' , `Bearer ${this.apiService.TokenRequest}` );


    const reqClone = req.clone({
      headers
    });
    return next.handle(reqClone)
    .pipe(
        retry(0),
        catchError((error: HttpErrorResponse) => {
          if (error.status === 401){
            if (this.router.url !== '/401' && this.router.url !== '403'){
              this.router.navigate(['/401'] , { replaceUrl : true });
            }
          }
          return throwError(error);
        })

    );
  }
}
