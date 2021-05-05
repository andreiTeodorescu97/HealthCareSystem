import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConsultationDto } from 'app/_models/consultationDto';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ConsultationService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  addConsultation(model: ConsultationDto) {
    return this.http.post(this.baseUrl + 'consultation/add', model);
  }

  getPacientConsultations(id : number) {
    return this.http.get<ConsultationDto[]>(this.baseUrl + 'consultation/get/' + id);
  }
}
