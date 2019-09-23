import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Feature } from 'src/app/Project/Models/feature';

@Injectable({
  providedIn: 'root'
})
export class FeatureService {

  BaseUrl: string = 'http://localhost:12481/api/Features/';

  constructor(private http: HttpClient) { }


  GetAllFeatures(): Observable<Feature[]> {
    return this.http.get<Feature[]>(this.BaseUrl);
  }


  GetFeatureById(id: number): Observable<Feature> {
    return this.http.get<Feature>(this.BaseUrl + id);
  }


  CreateFeature(feature: Feature): Observable<Feature> {
    return this.http.post<Feature>(this.BaseUrl, feature);
  }


  Delete(id: number): Observable<Feature> {
    return this.http.delete<Feature>(this.BaseUrl + id);
  }

  EditFeature(id:number, feature: Feature): Observable<Feature> {
    return this.http.put<Feature>(this.BaseUrl + id, feature);
  }
}
