import { AfterContentInit, Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { Transaction } from 'src/app/api/models';
import { ApiService } from 'src/app/api/services';
import { SpinnerService } from 'src/app/api/services/spinner.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent implements OnInit , AfterContentInit {

  displayedColumns: string[] = ['sku', 'amount' ];
  dataSource: Transaction[] = [];
  transactions: Transaction[] = [];

  constructor(
    public spinnerService: SpinnerService,
    private apiService : ApiService,
    private _snackBar: MatSnackBar,
    public route: ActivatedRoute,) { }


  ngAfterContentInit(): void {
     setTimeout(() => {
      this.route.params
        .subscribe(params => {
          const Sku = 'Sku';
          this.loadTransaction(params[Sku]);
      });
     }, 3000);

  }

  ngOnInit(): void {
     // obteniendo el parametro id URL

  }



  loadTransaction(sku:string) {
    this.spinnerService.startSpinner();
    this.apiService.transactionBySku({Sku: sku}).then(response =>  {
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

}
