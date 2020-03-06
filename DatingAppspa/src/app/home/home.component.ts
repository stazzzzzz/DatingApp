import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode = false;
  values : any = {};
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getValues()
  }

  registerToggle(){
    this.registerMode = true;
  }

  getValues() {
    this.http.get('https://localhost:5001/api/value').subscribe(response =>{this.values = response;},
    error => {console.log(error); });
  }

  cancelRegisterMethod(registerMode: boolean)
  {
    this.registerMode = registerMode;
  }
}
