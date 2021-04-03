import { DatePipe } from '@angular/common';
import { AbstractType, Component, OnInit } from '@angular/core';
import { Pacient } from 'app/_models/pacient';
import { PacientDto } from 'app/_models/registerDto';
import { User } from 'app/_models/user';
import { AccountService } from 'app/_services/account.service';
import { CityService } from 'app/_services/city.service';
import { PacientService } from 'app/_services/pacient.service';
import { take } from 'rxjs/operators';

@Component({
    selector: 'user-cmp',
    moduleId: module.id,
    templateUrl: 'user.component.html'
})

export class UserComponent implements OnInit {
    model: Pacient;
    user: User;
    cities: any;
    regions: any;

    constructor(private pacientService: PacientService, private accountService: AccountService, private cityService: CityService) {
        this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
    }

    ngOnInit(): void {
        this.getPacient();
        this.getCities();
        this.getRegions();
    }
    getPacient() {
        this.pacientService.getPacientByCnp(this.user.cnp)
            .subscribe(response => {
                this.model = response;
                var userdate: any = new Date(this.model.dateOfBirth).toDateString();
                var datePipe = new DatePipe('en-US')
                this.model.dateOfBirth = datePipe.transform(userdate, 'dd.MMM.yyyy');
            });
    }

    getCities() {
        this.cityService.getCities().subscribe(response => {
            this.cities = response;
            console.log(response);
        })
    }

    getRegions() {
        this.cityService.getRegions().subscribe(response => {
            this.regions = response;
            console.log(response);
        })
    }

    updateMember() {
        console.log(this.model);
    }
}
