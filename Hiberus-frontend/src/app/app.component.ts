import { AfterViewChecked, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ApiService } from './api/services';
import { SpinnerService } from './api/services/spinner.service';
import {
  MatSnackBar,
  MatSnackBarHorizontalPosition,
  MatSnackBarVerticalPosition,
} from '@angular/material/snack-bar';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit,  AfterViewChecked {

  horizontalPosition: MatSnackBarHorizontalPosition = 'left';
  verticalPosition: MatSnackBarVerticalPosition = 'top';
  isLoading = false;

  constructor(
    public spinnerService: SpinnerService,
    public cdRef: ChangeDetectorRef,
    private apiService : ApiService,
    private _snackBar: MatSnackBar
  ) {
  }


  ngOnInit(): void {
    this.spinnerService.startSpinner();
    this.apiService.token({Email : 'admin@username.com'}).then(response =>  {
      this.apiService.TokenRequest = response;
    }).catch(error => {
      this._snackBar.open(error.message , 'Close', {
        horizontalPosition: this.horizontalPosition,
        verticalPosition: this.verticalPosition,
      });
    });
  }


  ngAfterViewChecked() {
    this.spinnerService.spinnerVisibilityChange.subscribe(resp => {
      this.isLoading = resp;
      this.cdRef.detectChanges();
    });
  }


}
