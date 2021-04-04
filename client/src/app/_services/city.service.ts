import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { City } from 'app/_models/city';
import { Region } from 'app/_models/region';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CityService {

  baseUrl = environment.apiUrl;
  
  constructor(private http: HttpClient) { }

  getCities(){
    return this.http.get<City[]>(this.baseUrl + 'cities/cities');
  }

  getRegions(){
    return this.http.get<Region[]>(this.baseUrl + 'cities/regions');
  }
}
