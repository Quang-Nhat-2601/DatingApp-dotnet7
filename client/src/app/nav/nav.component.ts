import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit{
  model: any = {}  // this is used to store information
  currentUser$: Observable<User | null> = of(null);
  
  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
  }

  constructor(public accountService: AccountService) {
    
    console.log("navbar print")
    console.log("nav === ", this.currentUser$)
  }

  login() {
    this.accountService.login(this.model).subscribe({
      next: response => {
        console.log(response)
      },
      error: error => console.log(error)
    })
  }

  logout() {
    this.accountService.logout(); 
  }

}
