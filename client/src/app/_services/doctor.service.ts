import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DoctorDto } from 'app/_models/doctorDto';
import { DoctorGriDto } from 'app/_models/doctorGridDto';
import { UserDoctorDto } from 'app/_models/userDoctorDto';
import { WorkDayDto } from 'app/_models/workDayDto';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getDoctorByUsername(userName: string) {
    return this.http.get<UserDoctorDto>(this.baseUrl + 'doctors/' + userName);
  }

  updateDoctor(doctor: UserDoctorDto) {
    return this.http.put(this.baseUrl + 'doctors', doctor);
  }

  updateWorkDays(workDays: WorkDayDto[]) {
    return this.http.post(this.baseUrl + 'doctors/add-work-days', workDays);
  }

  getWorkDays() {
    return this.http.get<WorkDayDto[]>(this.baseUrl + 'doctors/work-days');
  }

  getDoctors() {
    return this.http.get<DoctorGriDto[]>(this.baseUrl + 'doctors');
  }

  getDoctorByDoctorId(id: string) {
    return this.http.get<DoctorDto>(this.baseUrl + 'doctors/getdoctor/?doctorId=' + id);
  }

  setMainPhoto(photoId: number) {
    return this.http.put(this.baseUrl + 'doctors/set-main-photo/' + photoId, {});
  }

  deletePhoto(photoId: number) {
    return this.http.delete(this.baseUrl + 'doctors/delete-photo/' + photoId);
  }

}
