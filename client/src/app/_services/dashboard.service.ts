import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AppoinmentReasonGraph } from 'app/_models/_dashboard/appoinmentReasonGraph';
import { DoctorStatistics } from 'app/_models/_dashboard/doctorStatistics';
import { PublisherStatisticsDto } from 'app/_models/_dashboard/publisherStatisticsDto';
import { VaccineGraph } from 'app/_models/_dashboard/vaccineGraph';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getPacientStatistics() {
    return this.http.get<PublisherStatisticsDto>(this.baseUrl + 'dashboard/pacient-statistics');
  }

  getReasonsStatistics() {
    return this.http.get<AppoinmentReasonGraph[]>(this.baseUrl + 'dashboard/doctor-appoinments-statistics');
  }

  getDoctorStatistics() {
    return this.http.get<DoctorStatistics>(this.baseUrl + 'dashboard/doctor-statistics');
  }

  getVaccinesStatistics() {
    return this.http.get<VaccineGraph[]>(this.baseUrl + 'dashboard/doctor-vaccines-statistics');
  }
}
