import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FreeHourDto } from 'app/_models/freehourDto';
import { GetAppoinmentDto } from 'app/_models/getAppoinmentDto';
import { MakeAnAppoinmentDto } from 'app/_models/makeAnAppoinmentDto';
import { UpdateAppoinmentStatusDto } from 'app/_models/updateAppoinmentStatusDto';
import { DoctorAppoinmentsFilterDto } from 'app/_models/_filters/doctorAppoinmentsFilterDto';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AppoinmentsService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAvailableHours(doctorId: string, unixTime: Number) {
    return this.http.get<FreeHourDto[]>(this.baseUrl + 'appoinments/getHours?doctorId=' + doctorId + "&unixTime=" + unixTime);
  }

  makeAnAppoinment(model: MakeAnAppoinmentDto) {
    return this.http.post(this.baseUrl + 'appoinments/add', model);
  }

  getAppoinmentsForDoctor(filterObj: DoctorAppoinmentsFilterDto) {
    return this.http.post<GetAppoinmentDto[]>(this.baseUrl + 'appoinments/doctorAppoinmets', filterObj);
  }

  getAppoinmentsForPacient() {
    return this.http.get<GetAppoinmentDto[]>(this.baseUrl + 'appoinments/pacientAppoinmets');
  }

  updateAppoinmentStatus(model: UpdateAppoinmentStatusDto) {
    return this.http.post(this.baseUrl + 'appoinments/updateStatus', model);
  }
}