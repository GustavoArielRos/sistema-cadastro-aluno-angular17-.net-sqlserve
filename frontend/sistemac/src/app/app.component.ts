import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';



import { HttpClientModule, HttpClient } from '@angular/common/http';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';



import { HomeComponent } from "./pages/home/home.component";

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    imports: 
      [
        CommonModule, 
        RouterOutlet, 
        HttpClientModule, 
        FormsModule, 
        ReactiveFormsModule, 
        HomeComponent,
       
       
      ]
})
export class AppComponent {
  title = 'sistemac';


  tasks: any = [];

  APIURL="http://localhost:8000/";

  constructor(private http:HttpClient){}

  ngOnInit(){
    this.get_tasks();
  }

  get_tasks(){
    this.http.get(this.APIURL+"get_tasks").subscribe((res) => {
      this.tasks=res;
      console.log(res);
    })
    
  }

}
