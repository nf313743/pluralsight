﻿import { Component, OnInit } from '@angular/core';
import { ProductService } from './product.service';
import { Product } from './product';
import { CategoryService } from '../category/category.service';
import { Category } from '../category/category';
import { ProductSearch } from './productSearch';
import { Router } from '@angular/router';
import { error } from 'selenium-webdriver';

@Component({
    templateUrl: './product-list.component.html'
})
export class ProductListComponent implements OnInit {
    constructor(
        private productService: ProductService,
        private categoryService: CategoryService,
        private router: Router) { }

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

    add() {
        this.router.navigate(['/productDetail', -1])
    }

    selectProduct(id: number) {
        this.router.navigate(['/productDetail', id])
    }

    deleteProduct(id: number) {
        if (confirm('Delete this product?')) {
            this.productService.deleteProduct(id)
                .subscribe(() => this.getProducts(),
                    errors => this.handleErrors(errors));
        }
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