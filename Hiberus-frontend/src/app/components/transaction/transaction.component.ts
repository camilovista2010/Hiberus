import { Router } from '@angular/router';
import { Transaction } from './../../api/models/transaction';
import { Component, OnInit } from '@angular/core';
import { SpinnerService } from 'src/app/api/services/spinner.service';
import { ApiService } from 'src/app/api/services';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.css']
})
export class TransactionComponent implements OnInit {

  displayedColumns: string[] = ['sku', 'amount' ];
  dataSource: Transaction[] = [
    {"id":"8aedad11-463d-45e4-8f0c-7eafae71de91","sku":"Y9047","amount":27.0,"currency":"EUR"},
    {"id":"13b2a5d8-5b7c-470c-9cd1-1ae57be77201","sku":"U4333","amount":25.0,"currency":"EUR"},
    {"id":"73fd364a-4f3f-41be-b04a-bff660184297","sku":"B0472","amount":21.6,"currency":"CAD"},
    {"id":"7f745eb5-195c-4f4d-aef4-f1de838dea85","sku":"N3485","amount":27.0,"currency":"EUR"}];
  transactions: Transaction[] = [
    {"id":"8aedad11-463d-45e4-8f0c-7eafae71de91","sku":"Y9047","amount":27.0,"currency":"EUR"},
    {"id":"13b2a5d8-5b7c-470c-9cd1-1ae57be77201","sku":"U4333","amount":25.0,"currency":"EUR"},
    {"id":"73fd364a-4f3f-41be-b04a-bff660184297","sku":"B0472","amount":21.6,"currency":"CAD"},
    {"id":"7f745eb5-195c-4f4d-aef4-f1de838dea85","sku":"N3485","amount":27.0,"currency":"EUR"}];

  constructor(
    public spinnerService: SpinnerService,
    private apiService : ApiService,
    private _snackBar: MatSnackBar,
    private router: Router
  ) { }

  ngOnInit(): void {
  }


  loadTransaction() {
    this.spinnerService.startSpinner();
    this.apiService.transaction().then(response =>  {
      this.dataSource = response.filter(x => x.currency === 'EUR');
      this.transactions = this.dataSource;
      this.spinnerService.stopSpinner();
    }).catch(error => {
      this._snackBar.open(error.message , 'Close');
    });
  }

  getTotalCost() {
    return this.transactions.map(t => t.amount).reduce( (acc, value) => { return acc as number + Number(value) ; }, 0);
  }

  applyFilter(value:Event) {
    const filterValue = (value.target as HTMLInputElement).value;
    // this.dataSource.filter = filterValue.trim().toLowerCase() ;
  }

  redirectToDetails(Sku:string) {
    this.router.navigate(['/detail/' + Sku ]);
  }
}
