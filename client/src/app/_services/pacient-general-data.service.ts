import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PacientGeneralMedicalDataDto } from 'app/_models/pacientGeneralMedicalDataDto';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PacientGeneralDataService {

  baseUrl = environment.apiUrl;
  
  constructor(private http: HttpClient) { }

  updatePacientGeneralData(pacientGeneralData: PacientGeneralMedicalDataDto){
    return this.http.post(this.baseUrl + 'pacientGeneralData/updateGeneralData', pacientGeneralData);
  }
}
