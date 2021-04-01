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
}

export const ROUTES: RouteInfo[] = [
    { path: '/register', title: 'Register', icon: 'nc-align-center', class: '' },
    { path: '/dashboard', title: 'Dashboard', icon: 'nc-bank', class: '' },
    { path: '/icons', title: 'Icons', icon: 'nc-diamond', class: '' },
    { path: '/maps', title: 'Maps', icon: 'nc-pin-3', class: '' },
    { path: '/notifications', title: 'Notifications', icon: 'nc-bell-55', class: '' },
    { path: '/user', title: 'User Profile', icon: 'nc-single-02', class: '' },
    { path: '/table', title: 'Table List', icon: 'nc-tile-56', class: '' },
    { path: '/typography', title: 'Typography', icon: 'nc-caps-small', class: '' },
    { path: '/upgrade', title: 'Upgrade to PRO', icon: 'nc-spaceship', class: 'active-pro' },
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
                this.menuItems = ROUTES.filter(menuItem => menuItem.title != 'Register');
            } else {
                this.menuItems = ROUTES.filter(menuItem => menuItem.title == 'Register');
            }
        })
    }
}
