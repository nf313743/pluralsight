import { Component, OnInit } from '@angular/core';
import { ProductService } from './product.service';
import { Product } from './product';
import { CategoryService } from '../category/category.service';
import { Category } from '../category/category';
import { ProductSearch } from './productSearch';

@Component({
    templateUrl: './product-list.component.html'
})
export class ProductListComponent implements OnInit {
    constructor(
        private productService: ProductService,
        private categoryService: CategoryService) { }

    ngOnInit() {
        this.searchEntity.categoryId = 0;
        this.getProducts();
        this.getSearchCategories();
    }

    products: Product[] = [];
    messages: string[] = [];
    searchCategories: Category[] = [];
    searchEntity: ProductSearch = new ProductSearch();

    private getProducts() {
        this.productService.getProducts()
            .subscribe(
                products => this.products = products,
                errors => this.handleErrors(errors))
    }

    search() {
        this.productService.search(this.searchEntity)
            .subscribe(x => this.products = x,
                errors => this.handleErrors(errors));
    }

    resetSearch() {
        this.searchEntity.categoryId = 0;
        this.searchEntity.productName = "";
        this.getProducts();
    }

    private getSearchCategories() {
        this.categoryService.getSearchCategories()
            .subscribe(x => this.searchCategories = x,
                errors => this.handleErrors(errors));
    }

    private handleErrors(errors: any): void {
        this.messages = [];

        for (let msg of errors) {
            this.messages.push(msg);
        }
    }
}