import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms'; 
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { provideHttpClient } from '@angular/common/http'; 
import { NgxPaginationModule } from 'ngx-pagination';

import   
 { routes } from './app.routes';   

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes),
    FormsModule,
    NgxPaginationModule
    
  ],
  providers: [provideHttpClient()],
  bootstrap: [AppComponent]
})
export class   
 AppModule { }