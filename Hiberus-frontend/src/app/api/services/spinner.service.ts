import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SpinnerService {

    // Spinner state
    loading = false;
    spinnerVisibilityChange: Subject<boolean> = new Subject<boolean>();

    constructor() {
      this.spinnerVisibilityChange.subscribe((value) => {
        this.loading = value;
      });
    }

    toggleSidebarVisibility() {
      this.spinnerVisibilityChange.next(!this.loading);
    }
    startSpinner() {
      this.spinnerVisibilityChange.next(true);
    }

    stopSpinner() {
      this.spinnerVisibilityChange.next(false);
    }

}
