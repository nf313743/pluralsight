import { Component, OnInit } from '@angular/core';
import { ProductService } from './product.service';
import { Product } from './product';

@Component({
    templateUrl: './product-list.component.html'
})
export class ProductListComponent implements OnInit {
    constructor(private productService: ProductService) {
    }

    ngOnInit() {
        this.getProducts();
    }

    products: Product[] = [];
    messages: string[] = [];

    private getProducts() {
        this.productService.getProducts()
            .subscribe(
                products => this.products = products,
                errors => this.handleErrors(errors))
    }

    private handleErrors(errors: any): void {
        this.messages = [];

        for (let msg of errors) {
            this.messages.push(msg);
        }
    }
}