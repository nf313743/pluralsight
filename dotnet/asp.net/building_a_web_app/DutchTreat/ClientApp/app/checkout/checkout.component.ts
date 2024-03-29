﻿import { Component } from "@angular/core";
import { DataService } from '../shared/dataService';
import { Router } from '@angular/router';

@Component({
  selector: "checkout",
  templateUrl: "checkout.component.html",
  styleUrls: ['checkout.component.css']
})
export class Checkout {
  errorMessage: string;

  constructor(public data: DataService, public router: Router) {
  }

  onCheckout() {
    this.data.checkout()
      .subscribe(success => {
        if (success) {
          this.router.navigate([""]);
        }
      },
        error => this.errorMessage = "Failed to save order");
  }
}