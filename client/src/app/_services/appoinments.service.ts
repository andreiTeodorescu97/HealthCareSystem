import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FreeHourDto } from 'app/_models/freehourDto';
import { MakeAnAppoinmentDto } from 'app/_models/makeAnAppoinmentDto';
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
}