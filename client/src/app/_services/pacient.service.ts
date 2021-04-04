import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pacient } from 'app/_models/pacient';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PacientService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getPacientByCnp(cnp: string) {
    return this.http.get<Pacient>(this.baseUrl + 'pacients/find/' + cnp);
  }

  updatePacient(pacient: Pacient) {
    return this.http.put(this.baseUrl + 'pacients', pacient);
  }


}
