import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from '../Models/category';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  BaseUrl: string = 'http://localhost:12481/api/Categories/';

  constructor(private http: HttpClient) { }

  GetAllCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.BaseUrl);
  }


  GetCategoryById(id: number): Observable<Category> {
    return this.http.get<Category>(this.BaseUrl + id);
  }


  CreateCategory(category: Category): Observable<Category> {
    return this.http.post<Category>(this.BaseUrl, category);
  }


  Delete(id: number): Observable<Category> {
    return this.http.delete<Category>(this.BaseUrl + id);
  }

  EditCategory(id: number,category: Category): Observable<Category> {
    return this.http.put<Category>(this.BaseUrl + id, category);
  }

}
