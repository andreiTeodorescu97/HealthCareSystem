import { Component, OnInit } from '@angular/core';
import { User } from 'app/_models/user';
import { AccountService } from 'app/_services/account.service';
import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';


export interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
    isRegisterRequired: boolean;
}

export const ROUTES: RouteInfo[] = [
    { path: '/register', title: 'Inregistrare Pacient', icon: 'nc-icon nc-badge', class: '', isRegisterRequired: false },
    { path: '/docregister', title: 'Inregistrare Doctor', icon: 'nc-icon nc-ambulance', class: '', isRegisterRequired: false },
    { path: '/dashboard', title: 'Dashboard', icon: 'nc-icon nc-bank', class: '', isRegisterRequired: true },
    { path: '/doctor/appoinments', title: 'Programari Doctor', icon: 'fa fa-clock-o', class: '', isRegisterRequired: true },
    { path: '/pacient/appoinments', title: 'Programari Pacient', icon: 'fa fa-clock-o', class: '', isRegisterRequired: true },
    { path: '/doctors', title: 'Doctori', icon: 'fa fa-user-md', class: '', isRegisterRequired: true },
    { path: '/pacient/pacient_medical_data', title: 'Fisa Medicala', icon: 'fa fa-book', class: '', isRegisterRequired: true },
    { path: '/upgrade', title: 'Newsletter', icon: 'fa fa-envelope', class: 'active-pro', isRegisterRequired: true },
    { path: '/user', title: 'Profil Pacient', icon: 'fa fa-user', class: '', isRegisterRequired: true },
    { path: '/doctor_work_days', title: 'Orar Doctor', icon: 'fa fa-calendar', class: '', isRegisterRequired: true },
    { path: '/doctor_profile', title: 'Profil Doctor', icon: 'fa fa-user', class: '', isRegisterRequired: true },
];

@Component({
    moduleId: module.id,
    selector: 'sidebar-cmp',
    templateUrl: 'sidebar.component.html',
})

export class SidebarComponent implements OnInit {
    public menuItems: any[];
    user: User;

    constructor(private accountService: AccountService) { }

    ngOnInit() {
        this.accountService.currentUser$.subscribe((user: User) => {
            if (user) {
                this.user = user;
                this.menuItems = ROUTES.filter(menuItem => menuItem.isRegisterRequired == true);
            } else {
                this.menuItems = ROUTES.filter(menuItem => menuItem.isRegisterRequired == false);
            }
        })
    }
}
