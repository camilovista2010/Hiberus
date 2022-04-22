import { AppModule } from './../../app.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { RatesComponent } from 'src/app/components/rates/rates.component';
import { TransactionComponent } from 'src/app/components/transaction/transaction.component';
import { HomeRoutingModule } from './home-routing.module';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatTableModule} from '@angular/material/table';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import {MatButtonModule} from '@angular/material/button';
import {MatTabsModule} from '@angular/material/tabs';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatIconModule} from '@angular/material/icon';



@NgModule({
  declarations: [
    HomeComponent,
    RatesComponent,
    TransactionComponent,
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    MatGridListModule,
    MatTableModule,
    MatSnackBarModule,
    MatButtonModule,
    MatTabsModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule
  ]
})
export class HomeModule { }
