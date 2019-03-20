import { Injectable } from "@angular/core";
import { Http, Response } from '@angular/http';
import { Observable } from "rxjs/Observable";
import { Product } from "./product";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { defaultCipherList } from "constants";

@Injectable()
export class ProductService {
    private url = '/api/productApi';

    constructor(private http: Http) { }

    getProducts(): Observable<Product[]> {
        return this.http.get(this.url)
            .map(this.extractData)
            .catch(this.handleErrors);
    }

    private extractData(res: Response) {
        let body = res.json();
        return body || {};
    }

    private handleErrors(error: any): Observable<any> {
        let errors: string[] = [];

        switch (error.status) {
            case 400:
                let err = error.json();
                if (err.message) {
                    errors.push(err.messsages);
                }
                else {
                    errors.push('An unknown error occurred.');
                }
                break;
            case 404:
                errors.push('No product date is available.');
                break;
            case 500:
                errors.push(error.json().exceptionMessage);
                break;

            default:
                errors.push('Status: ' + error.status + ' - Error Message: ' + error.statusText);
                break;
        }
        console.error('An error occuerred', errors);
        return Observable.throw(errors);
    }
}