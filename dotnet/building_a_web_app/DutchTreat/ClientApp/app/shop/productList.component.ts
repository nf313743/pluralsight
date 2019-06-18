import { Component } from '@angular/core';

@Component({
    selector:"product-list",
    templateUrl: "productList.component.html",
    styleUrls:[]
})
export class ProductList{
    public products = [{
        title:"First Product",
        price: 19.9}, 
        {
            title:"Second Product",
            price: 19.9
        },
        {
            title:"third Product",
            price: 19.9
        }];
}