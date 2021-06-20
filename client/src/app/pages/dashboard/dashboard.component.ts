import { Component, OnInit } from '@angular/core';
import { User } from 'app/_models/user';
import { DoctorStatistics } from 'app/_models/_dashboard/doctorStatistics';
import { PublisherStatisticsDto } from 'app/_models/_dashboard/publisherStatisticsDto';
import { AccountService } from 'app/_services/account.service';
import { DashboardService } from 'app/_services/dashboard.service';
import Chart from 'chart.js';
import { take } from 'rxjs/operators';


@Component({
  selector: 'dashboard-cmp',
  moduleId: module.id,
  templateUrl: 'dashboard.component.html'
})

export class DashboardComponent implements OnInit {

  public canvas: any;
  public ctx;
  public chartColor;
  public chartEmail;
  public chartHours;
  pacientStats: PublisherStatisticsDto;
  doctorStats: DoctorStatistics;
  user: User;
  date: Date;

  constructor(private dashboardService: DashboardService, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit() {
    this.date = new Date();
    if (this.user.roles.includes('Pacient')) {
      this.getPacientStatistics();
    } else if (this.user.roles.includes('Doctor')) {

      this.getDoctorStatistics();
    }
  }

  getPacientStatistics() {
    this.dashboardService.getPacientStatistics().subscribe(data => {
      this.pacientStats = data;
    })
  }

  getDoctorStatistics() {
    this.dashboardService.getDoctorStatistics().subscribe(data => {
      this.doctorStats = data;
    })
  }
}
