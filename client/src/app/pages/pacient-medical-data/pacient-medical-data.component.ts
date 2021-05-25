import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'app/_models/user';
import { AccountService } from 'app/_services/account.service';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-pacient-medical-data',
  templateUrl: './pacient-medical-data.component.html',
  styleUrls: ['./pacient-medical-data.component.css']
})
export class PacientMedicalDataComponent implements OnInit {

  user:User;
  
  constructor(private accountService: AccountService, private router: Router) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user); 
  }

  ngOnInit(): void {
    this.router.navigateByUrl('pacient/pacient_profile/' + this.user.id);
  }

}
