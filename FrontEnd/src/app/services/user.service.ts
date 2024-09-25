import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserLoginModel, UserRegisterModel } from '../models/user.model'; 

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'https://localhost:7291/api/User';

  constructor(private http: HttpClient) { }

  // Registro de usuario
  register(registerModel: UserRegisterModel): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, registerModel);
  }

  // Loguear usuario
  login(loginModel: UserLoginModel): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, loginModel);
  }

  // Obtener todos los usuarios
  getAllUsers(token: string): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.apiUrl}/all`, { headers });
  }

  // Obtener usuario por ID
  getUserById(id: string, token: string): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.apiUrl}/${id}`, { headers });
  }

  // Borrar usuario por ID
  deleteUser(id: string, token: string): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.delete(`${this.apiUrl}/${id}`, { headers });
  }
}
