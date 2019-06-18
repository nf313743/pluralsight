import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Product } from './product';
import { Observable } from 'rxjs';
import * as OrderNs from './order';

@Injectable()
export class DataService {

    constructor(private http: HttpClient) { }

    public products: Product[] = [];

    public order: OrderNs.Order = new OrderNs.Order();

    loadProducts(): Observable<boolean> {
        return this.http.get("/api/products")
            .pipe(
                map((data: Product[]) => {
                    this.products = data;
                    return true;
                }));
    }

    public addToOrder(newProduct: Product) {
        let item: OrderNs.OrderItem = this.order.items.find(x => x.productId == newProduct.id);

        if (item) {
            item.quantity++;
        }
        else {
            item = new OrderNs.OrderItem()
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
}
