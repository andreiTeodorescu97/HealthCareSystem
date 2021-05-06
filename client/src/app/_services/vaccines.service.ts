import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UpdatePacientVaccinesDto } from 'app/_models/updatePacientVaccinesDto';
import { VaccineDto } from 'app/_models/vaccineDto';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class VaccinesService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getRequiredVaccines() {
    return this.http.get<VaccineDto[]>(this.baseUrl + 'vaccines/required');
  }

  getMadeVaccines(id: number) {
    return this.http.get<VaccineDto[]>(this.baseUrl + 'vaccines/userVaccines/' + id);
  }

  updatePacientVaccines(model: UpdatePacientVaccinesDto){
    return this.http.post(this.baseUrl + 'vaccines/add', model);
  }
}
