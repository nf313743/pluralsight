import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Product } from './product';
import { Observable } from 'rxjs';
import * as OrderNs from './order';

@Injectable()
export class DataService {

    constructor(private http: HttpClient) { }

    private token: string = ""
    private tokenExpiration: Date;

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

    public get loginRequired(): boolean {
        return this.token.length === 0 || this.tokenExpiration > new Date();
    }

    login(creds): Observable<boolean> {
        return this.http.post("/account/createtoken", creds)
            .pipe(
                map((data: any) => {
                    this.token = data.token;
                    this.tokenExpiration = data.expiration;
                    return true;
                }));
    }

    public checkout() {
        if (!this.order.orderNumber) {
            this.order.orderNumber = this.order.orderDate.getFullYear().toString() + this.order.orderDate.getTime().toString();
        }
        return this.http.post("/api/orders", this.order, {
            headers: new HttpHeaders().set("Authorization", "Bearer " + this.token)
        })
            .pipe(
                map(response => {
                    this.order = new OrderNs.Order();
                    return true;
                }));
    }

    public addToOrder(newProduct: Product) {
        let item: OrderNs.OrderItem = this.order.items.find(x => x.productId === newProduct.id);

        if (item) {
            item.quantity++;
        } else {
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
