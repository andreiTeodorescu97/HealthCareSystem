import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CityService {

  baseUrl = environment.apiUrl;
  
  constructor(private http: HttpClient) { }

  getCities(){
    return this.http.get(this.baseUrl + 'cities/cities');
  }

  getRegions(){
    return this.http.get(this.baseUrl + 'cities/regions');
  }
}
