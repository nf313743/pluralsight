import { Component, OnInit } from '@angular/core';
import { IProduct } from './product';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from './product.service';

@Component({
    templateUrl: './product-detail.component.html',
    styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {
    pageTitle: string = "Product Detail";
    product: IProduct;
    errorMessage: string;

    constructor(
        private route:ActivatedRoute,
        private productService: ProductService,
        private router:Router) { }

    ngOnInit() {
        let id = +this.route.snapshot.paramMap.get('id');
         this.productService.getProduct(id).subscribe(
            product => {
                 this.product = product
             },
             error => this.errorMessage = <any>error
         );
    }

    onBack() :void{
        this.router.navigate(['/products']);
    }

}
