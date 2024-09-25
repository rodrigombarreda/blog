import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NewEntry,Entry } from '../models/entry.model';

@Injectable({
  providedIn: 'root'
})
export class EntryService {
  private apiUrl = 'https://localhost:7291/api/BlogEntry'; 

  constructor(private http: HttpClient) {}

  getEntries(page: number, pageSize: number, filter: string): Observable<{ entries: Entry[], total: number }> {
    const filterParam = filter ? `&filter=${filter}` : ''; 
    const token = localStorage.getItem('jwtToken');
    const headers = token ? new HttpHeaders().set('Authorization', `Bearer ${token}`) : {};

    return this.http.get<{ entries: Entry[], total: number }>(
      `${this.apiUrl}?page=${page}&pageSize=${pageSize}${filterParam}`,
      { headers } 
    );
  }
  



  createEntry(entry: NewEntry): Observable<NewEntry> {
    const token = localStorage.getItem('jwtToken');
    const headers = token ? new HttpHeaders().set('Authorization', `Bearer ${token}`) : {};
    return this.http.post<NewEntry>(`${this.apiUrl}`, entry, { headers }); 
  }

  deleteEntry(entryId: string) {
    const token = localStorage.getItem('jwtToken');
    const headers = token ? new HttpHeaders().set('Authorization', `Bearer ${token}`) : {};
    return this.http.delete(`${this.apiUrl}/${entryId}`,{ headers });
  }

  updateEntry(entry: NewEntry): Observable<NewEntry> {


    const token = localStorage.getItem('jwtToken');
    const headers = token ? new HttpHeaders().set('Authorization', `Bearer ${token}`) : {};

    return this.http.put<NewEntry>(`${this.apiUrl}/${entry.id}`, entry, { headers });
  }
  
}
