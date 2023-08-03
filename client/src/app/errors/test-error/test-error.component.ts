import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.css']
})

export class TestErrorComponent {
  baseUrl = "https://localhost:5001/api/";
  validationErrors: string[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {

  }

  get404Error() {
    this.http.get(this.baseUrl + "buggy/not-found").subscribe({
      next: reponse => console.log(reponse), // wont be returned
      error: error => console.log(error)
    })
  }

  get400Error() {
    this.http.get(this.baseUrl + "buggy/bad-request").subscribe({
      next: reponse => console.log(reponse), // wont be returned
      error: error => console.log(error)
    })
  }

  get500Error() {
    this.http.get(this.baseUrl + "buggy/server-error").subscribe({
      next: reponse => console.log(reponse), // wont be returned
      error: error => console.log(error)
    })
  }

  get401Error() {
    this.http.get(this.baseUrl + "buggy/auth").subscribe({
      next: reponse => console.log(reponse), // wont be returned
      error: error => console.log(error)
    })
  }

  get400ValidationError() {
    this.http.post(this.baseUrl + "account/register", {}).subscribe({
      next: reponse => console.log(reponse), // wont be returned
      error: error => {
        console.log(error);
        this.validationErrors = error;
      }
    })
  }


}
