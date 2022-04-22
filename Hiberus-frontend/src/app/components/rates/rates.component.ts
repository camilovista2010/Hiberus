import { Rate } from './../../api/models/rate';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApiService } from 'src/app/api/services';
import { SpinnerService } from 'src/app/api/services/spinner.service';

@Component({
  selector: 'app-rates',
  templateUrl: './rates.component.html',
  styleUrls: ['./rates.component.css']
})
export class RatesComponent implements OnInit {

  displayedColumns: string[] = ['from', 'to', 'rate'];
  dataSource: Rate[] = [];

  constructor(
    public spinnerService: SpinnerService,
    private apiService : ApiService,
    private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.loadRate();
  }


  loadRate() {
    this.spinnerService.startSpinner();
    this.apiService.rate().then(response =>  {
      this.dataSource = response;
      this.spinnerService.stopSpinner();
    }).catch(error => {
      this._snackBar.open(error.message , 'Close');
    });
  }

}
