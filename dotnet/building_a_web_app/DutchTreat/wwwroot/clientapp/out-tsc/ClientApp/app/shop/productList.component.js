import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
import { DataService } from '../shared/dataService';
let ProductList = class ProductList {
    constructor(data) {
        this.data = data;
        this.products = [];
    }
    ngOnInit() {
        this.data.loadProducts()
            .subscribe((succes) => {
            if (succes) {
                this.products = this.data.products;
            }
            ;
        });
    }
    addProduct(product) {
        this.data.addToOrder(product);
    }
};
ProductList = tslib_1.__decorate([
    Component({
        selector: "product-list",
        templateUrl: "productList.component.html",
        styleUrls: ["productList.component.css"]
    }),
    tslib_1.__metadata("design:paramtypes", [DataService])
], ProductList);
export { ProductList };
//# sourceMappingURL=productList.component.js.map