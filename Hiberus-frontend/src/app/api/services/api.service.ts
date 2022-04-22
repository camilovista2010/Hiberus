/* tslint:disable */
/* eslint-disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';

import { Rate } from '../models/rate';
import { Transaction } from '../models/transaction';

@Injectable({
  providedIn: 'root',
})
export class ApiService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }
  readonly RatePath = '/Rate';
  readonly TransactionPath = '/Transaction';
  readonly TransactionBySkuPath = '/transaction';
  readonly TokenPath = '/token';

  public TokenRequest : string = "";

  rate(): Promise<Array<Rate>> {
    return this.http.get<Array<Rate>>(`${this.rootUrl}${this.RatePath}`).toPromise()
  }

  transaction(): Promise<Array<Transaction>> {
    return this.http.get<Array<Transaction>>(`${this.rootUrl}${this.TransactionPath}`).toPromise() ;
  }

  transactionBySku(params: {
    Sku: any;
  }): Promise<Array<Transaction>> {
    return this.http.get<Array<Transaction>>(`${this.rootUrl}/${params.Sku + this.TransactionBySkuPath}`).toPromise() ;
  }

  token(params: {
    Email: string;
  }) {
    return this.http.post(`${this.rootUrl}/${params.Email + this.TokenPath}` , {}, {responseType: 'text'})
    .toPromise()
    .then(data => data.toString()) ;
  }




}
