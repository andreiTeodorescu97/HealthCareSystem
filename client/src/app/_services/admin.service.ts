import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'app/_models/user';
import { AdminUserDto } from 'app/_models/_admin/adminUserDto';
import { environment } from 'environments/environment';
import { ReplaySubject } from 'rxjs';
import { PresenceService } from './presence.service';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router, private presence: PresenceService) { }

  block(userName: string) {
    return this.http.post(this.baseUrl + 'admin/block?userName=' + userName, {});
  }

  unblock(userName: string) {
    return this.http.post(this.baseUrl + 'admin/unblock?userName=' + userName, {});
  }

  getUserWithRoles() {
    return this.http.get<AdminUserDto[]>(this.baseUrl + 'admin/users-with-roles');
  }
}
