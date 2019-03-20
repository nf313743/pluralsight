"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var product_service_1 = require("./product.service");
var category_service_1 = require("../category/category.service");
var productSearch_1 = require("./productSearch");
var ProductListComponent = /** @class */ (function () {
    function ProductListComponent(productService, categoryService) {
        this.productService = productService;
        this.categoryService = categoryService;
        this.products = [];
        this.messages = [];
        this.searchCategories = [];
        this.searchEntity = new productSearch_1.ProductSearch();
    }
    ProductListComponent.prototype.ngOnInit = function () {
        this.searchEntity.categoryId = 0;
        this.getProducts();
        this.getSearchCategories();
    };
    ProductListComponent.prototype.getProducts = function () {
        var _this = this;
        this.productService.getProducts()
            .subscribe(function (products) { return _this.products = products; }, function (errors) { return _this.handleErrors(errors); });
    };
    ProductListComponent.prototype.search = function () {
        var _this = this;
        this.productService.search(this.searchEntity)
            .subscribe(function (x) { return _this.products = x; }, function (errors) { return _this.handleErrors(errors); });
    };
    ProductListComponent.prototype.resetSearch = function () {
        this.searchEntity.categoryId = 0;
        this.searchEntity.productName = "";
        this.getProducts();
    };
    ProductListComponent.prototype.getSearchCategories = function () {
        var _this = this;
        this.categoryService.getSearchCategories()
            .subscribe(function (x) { return _this.searchCategories = x; }, function (errors) { return _this.handleErrors(errors); });
    };
    ProductListComponent.prototype.handleErrors = function (errors) {
        this.messages = [];
        for (var _i = 0, errors_1 = errors; _i < errors_1.length; _i++) {
            var msg = errors_1[_i];
            this.messages.push(msg);
        }
    };
    ProductListComponent = __decorate([
        core_1.Component({
            templateUrl: './product-list.component.html'
        }),
        __metadata("design:paramtypes", [product_service_1.ProductService,
            category_service_1.CategoryService])
    ], ProductListComponent);
    return ProductListComponent;
}());
exports.ProductListComponent = ProductListComponent;
//# sourceMappingURL=product-list.component.js.map