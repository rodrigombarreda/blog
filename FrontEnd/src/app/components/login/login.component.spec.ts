import { Component } from '@angular/core';
import { UserService } from '../../services/user.service';
import { UserLoginModel } from '../../models/user.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent {
  loginData: UserLoginModel = { email: '', password: '' };

  constructor(private userService: UserService) { }

  onSubmit() {
    this.userService.login(this.loginData).subscribe(
      (response) => {
        console.log('Login exitoso', response);
        localStorage.setItem('token', response.token); // Guarda el token
      },
      (error) => {
        console.error('Error en el login', error);
      }
    );
  }
}
