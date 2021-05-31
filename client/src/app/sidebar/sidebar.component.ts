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
    roles?: string[];
}

export const ROUTES: RouteInfo[] = [
    { path: '/register', title: 'Inregistrare Pacient', icon: 'nc-icon nc-badge', class: '', isRegisterRequired: false },
    { path: '/docregister', title: 'Inregistrare Doctor', icon: 'nc-icon nc-ambulance', class: '', isRegisterRequired: false },
    { path: '/dashboard', title: 'Dashboard', icon: 'nc-icon nc-bank', class: '', isRegisterRequired: true, roles: ["Doctor", "Pacient", "Admin"] },
    { path: '/doctor/appoinments', title: 'Programari', icon: 'fa fa-clock-o', class: '', isRegisterRequired: true, roles: ["Doctor"] },
    { path: '/pacient/appoinments', title: 'Programari', icon: 'fa fa-clock-o', class: '', isRegisterRequired: true, roles: ["Pacient"] },
    { path: '/doctors', title: 'Doctori', icon: 'fa fa-user-md', class: '', isRegisterRequired: true, roles: ["Pacient"] },
    { path: '/pacient/pacient_medical_data', title: 'Fisa Medicala', icon: 'fa fa-book', class: '', isRegisterRequired: true, roles: ["Pacient"] },
    { path: '/upgrade', title: 'Newsletter', icon: 'fa fa-envelope', class: 'active-pro', isRegisterRequired: true, roles: ["Doctor", "Pacient"] },
    { path: '/messages', title: 'Mesaje', icon: 'fa fa-commenting', class: '', isRegisterRequired: true, roles: ["Doctor", "Pacient"] },
    { path: '/user', title: 'Profil', icon: 'fa fa-user', class: '', isRegisterRequired: true, roles: ["Pacient"] },
    { path: '/doctor_work_days', title: 'Orar', icon: 'fa fa-calendar', class: '', isRegisterRequired: true, roles: ["Doctor"] },
    { path: '/doctor_profile', title: 'Profil', icon: 'fa fa-user', class: '', isRegisterRequired: true, roles: ["Doctor"] },
    { path: '/admin', title: 'Admin', icon: 'fa fa-user-secret', class: '', isRegisterRequired: true, roles: ["Admin"] }
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
