import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms'
import { AppComponent } from './app.component';
import { ProductListComponent } from './products/product-list.component';
import { ConverToSpacesPipe } from './shared/convert-to-spaces-pipe';
import { StarComponent } from './shared/star.component';
import { HttpClientModule } from '@angular/common/http';
import { ProductDetailComponent } from './products/product-detail.component';
import { RouterModule } from '@angular/router'
import { WelcomeComponent } from './home/welcome.component';
import { ProductGuardService } from './products/product-guard.service';


@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        HttpClientModule,
        RouterModule.forRoot([
            { path: 'products', component: ProductListComponent },
            {
                path: 'products/:id',
                canActivate: [ProductGuardService],
                component: ProductDetailComponent
            },
            { path: 'welcome', component: WelcomeComponent },
            { path: '', redirectTo: 'welcome', pathMatch: 'full' },
            { path: '**', redirectTo: 'welcome', pathMatch: 'full' },
        ])
    ],
    declarations: [
        AppComponent,
        ProductListComponent,
        ConverToSpacesPipe,
        StarComponent,
        ProductDetailComponent,
        WelcomeComponent],
    providers: [ProductGuardService],
    bootstrap: [AppComponent]
})
export class AppModule { }
