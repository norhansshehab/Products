import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../Models/product';
import { Feature } from '../Models/feature';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  BaseUrl: string = 'http://localhost:12481/api/Products/';

  constructor(private http: HttpClient) { }

  GetAllProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.BaseUrl);
  }


  GetProductById(id: number): Observable<Product> {
    return this.http.get<Product>(this.BaseUrl + id);
  }


  CreateProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(this.BaseUrl, product);
  }


  Delete(id: number): Observable<Product> {
    return this.http.delete<Product>(this.BaseUrl + id);
  }

  EditProduct(product: Product): Observable<Product> {
    return this.http.put<Product>(this.BaseUrl, product);
  }

  GetFeaturesForCategory(id:number){
    return this.http.get<Feature[]>(this.BaseUrl+ "GetFeaturesForSelectedCategory/" + id);
  }
}
