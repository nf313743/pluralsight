import * as tslib_1 from "tslib";
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import * as OrderNs from './order';
let DataService = class DataService {
    constructor(http) {
        this.http = http;
        this.products = [];
        this.order = new OrderNs.Order();
    }
    loadProducts() {
        return this.http.get("/api/products")
            .pipe(map((data) => {
            this.products = data;
            return true;
        }));
    }
    addToOrder(newProduct) {
        let item = this.order.items.find(x => x.productId == newProduct.id);
        if (item) {
            item.quantity++;
        }
        else {
            item = new OrderNs.OrderItem();
            item.productId = newProduct.id;
            item.productArtist = newProduct.artist;
            item.productArtId = newProduct.artId;
            item.productCategory = newProduct.category;
            item.productTitle = newProduct.title;
            item.unitPrice = newProduct.price;
            item.quantity = 1;
            this.order.items.push(item);
        }
    }
};
DataService = tslib_1.__decorate([
    Injectable(),
    tslib_1.__metadata("design:paramtypes", [HttpClient])
], DataService);
export { DataService };
//# sourceMappingURL=dataService.js.map