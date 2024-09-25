import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms'; 
import { UserService } from '../../services/user.service'; 
import { UserLoginModel } from '../../models/user.model'; 
import { Router } from '@angular/router'; 

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  standalone: true,
  imports: [FormsModule] 
})
export class LoginComponent {
  loginData: UserLoginModel = {
    email: '',
    password: ''
  };

  constructor(private userService: UserService, private router: Router) {}

  onSubmit() {
    this.userService.login(this.loginData).subscribe({
      next: (response) => {
        console.log('Login successful', response);
        
        const token = response.token;
        const userId = response.userId; 
  
        localStorage.setItem('jwtToken', token);
        localStorage.setItem('userId', userId);
        this.router.navigate(['/entry-list']);

      },
      error: (error) => {
        console.error('Login failed', error);
      }
    });
  }
  

  goToRegister() {
    this.router.navigate(['/register']);
  }
}
