import { Component } from '@angular/core';
import { UserService } from '../../services/user.service'; 
import { UserRegisterModel } from '../../models/user.model';
import { FormsModule } from '@angular/forms'; 
import { CommonModule } from '@angular/common'; 
import { Router } from '@angular/router'; 

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  standalone: true,
  imports: [FormsModule,CommonModule ] 
})
export class RegisterComponent {
  registerData: UserRegisterModel = {
    firstName: '',
    lastName: '',
    email: '',
    password: '',
  };

  errorMessage: string = '';

  constructor(private userService: UserService,private router: Router) {}

  onSubmit() {
    this.userService.register(this.registerData).subscribe({
      next: (response) => {
        console.log('Registration successful', response);
        this.errorMessage = '';
        this.router.navigate(['/login']);
        
      },
      error: (error) => {
        console.error('Registration failed', error);
        this.errorMessage = 'Registration failed. Please try again.'; 
      }
    });
  }

  goToLogin() {
    this.router.navigate(['/login']); 
  }
}
