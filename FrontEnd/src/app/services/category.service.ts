import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from '../models/category.model'; 
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  private apiUrl = 'https://localhost:7291/api/category'; 

  constructor(private http: HttpClient) {}

  getAllCategories(): Observable<Category[]> {
    const token = localStorage.getItem('jwtToken');
    const headers = token ? new HttpHeaders().set('Authorization', `Bearer ${token}`) : {};
    return this.http.get<Category[]>(this.apiUrl, {  headers });
  }

  createCategory(description: string): Observable<Category> {
    const token = localStorage.getItem('jwtToken');
    const headers = new HttpHeaders()
        .set('Authorization', `Bearer ${token}`)
        .set('Content-Type', 'application/json');

    return this.http.post<Category>(this.apiUrl, JSON.stringify(description), { headers });
}
}
