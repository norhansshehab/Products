import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FeaturesComponent } from './Project/features/features.component';
import { CategoriesComponent } from './Project/categories/categories.component';
import { ProductsComponent } from './Project/products/products.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule, MatMenuModule, MatIconModule, MatSelectModule } from '@angular/material';
import { MyFilterPipe } from './Project/Filters/MyFilterPipe';


const appRoutes: Routes = [
  {
    path: '',
    redirectTo: '/Product',
    pathMatch: 'full'
  },
  { path: 'Feature', component: FeaturesComponent },
  { path: 'Category', component: CategoriesComponent },
  { path: 'Product', component: ProductsComponent },
  {
    path: '**',
    redirectTo: '/Product',
    pathMatch: 'full'
  }
]

@NgModule({
  declarations: [
    AppComponent,
    FeaturesComponent,
    CategoriesComponent,
    ProductsComponent,
    MyFilterPipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes),
    BrowserAnimationsModule, MatButtonModule, MatMenuModule, MatIconModule, MatSelectModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
