import { Component, OnInit } from '@angular/core';
import { Pacient } from 'app/_models/pacient';
import { PacientDto } from 'app/_models/registerDto';
import { User } from 'app/_models/user';
import { AccountService } from 'app/_services/account.service';
import { PacientService } from 'app/_services/pacient.service';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-pacient-profile',
  templateUrl: './pacient-profile.component.html',
  styleUrls: ['./pacient-profile.component.css']
})
export class PacientProfileComponent implements OnInit {

  pacient: Pacient;
  user: User;

  constructor(private pacientService: PacientService, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

   getPacient() {
    this.pacientService.getPacientByCnp(this.user.cnp)
        .subscribe(response => this.pacient = response);
}

  ngOnInit(): void {
    this.getPacient();
  }

}
