import { Component, OnInit } from '@angular/core';
import { User } from 'app/_models/user';
import { AccountService } from 'app/_services/account.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';


export interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
    isRegisterRequired: boolean;
}

export const ROUTES: RouteInfo[] = [
    { path: '/register', title: 'Inregistrare Pacient', icon: 'nc-badge', class: '', isRegisterRequired: false },
    { path: '/docregister', title: 'Inregistrare Doctor', icon: 'nc-ambulance', class: '', isRegisterRequired: false },
    { path: '/dashboard', title: 'Dashboard', icon: 'nc-bank', class: '', isRegisterRequired: true },
    { path: '/icons', title: 'Icons', icon: 'nc-diamond', class: '', isRegisterRequired: true },
    { path: '/maps', title: 'Maps', icon: 'nc-pin-3', class: '', isRegisterRequired: true },
    { path: '/notifications', title: 'Notifications', icon: 'nc-bell-55', class: '', isRegisterRequired: true },
    { path: '/table', title: 'Table List', icon: 'nc-tile-56', class: '', isRegisterRequired: true },
    { path: '/typography', title: 'Typography', icon: 'nc-caps-small', class: '', isRegisterRequired: true },
    { path: '/upgrade', title: 'Upgrade to PRO', icon: 'nc-spaceship', class: 'active-pro', isRegisterRequired: true },
    { path: '/errors', title: 'Errors', icon: 'nc-spaceship', class: '', isRegisterRequired: true },
    { path: '/user', title: 'Profil Pacient', icon: 'nc-single-02', class: '', isRegisterRequired: true },
    { path: '/doctor_work_days', title: 'Orar', icon: 'nc-time-alarm', class: '', isRegisterRequired: true },
    { path: '/doctor_profile', title: 'Profil Doctor', icon: 'nc-single-02', class: '', isRegisterRequired: true },
];

@Component({
    moduleId: module.id,
    selector: 'sidebar-cmp',
    templateUrl: 'sidebar.component.html',
})

export class SidebarComponent implements OnInit {
    public menuItems: any[];
    currentUser$: Observable<User>;

    constructor(private accountService: AccountService) { }

    ngOnInit() {
        this.accountService.currentUser$.subscribe((user: User) => {
            if (user) {
                this.menuItems = ROUTES.filter(menuItem => menuItem.isRegisterRequired == true);
            } else {
                this.menuItems = ROUTES.filter(menuItem => menuItem.isRegisterRequired == false);
            }
        })
    }
}
