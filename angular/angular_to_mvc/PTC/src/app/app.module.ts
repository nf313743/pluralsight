import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import { ProductListComponent } from './product/product-list.component';
import { ProductService } from './product/product.service';

@NgModule({
    imports: [BrowserModule, AppRoutingModule, HttpModule],
    declarations: [AppComponent, ProductListComponent],
    bootstrap: [AppComponent],
    providers: [ProductService]
})
export class AppModule { }