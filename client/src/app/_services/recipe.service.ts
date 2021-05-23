import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MedicineDto } from 'app/_models/_recipe/medicineDto';
import { RecipeDto } from 'app/_models/_recipe/recipeDto';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  baseUrl = environment.apiUrl;
  
  constructor(private http: HttpClient) { }

  addRecipe(model: RecipeDto) {
    return this.http.post(this.baseUrl + 'recipe/add', model);
  }

  getPacientConsultations(id : number) {
    return this.http.get<RecipeDto>(this.baseUrl + 'recipe/get-recipe?recipeId=' + id);
  }

  getMedicines(){
    return this.http.get<MedicineDto[]>(this.baseUrl + 'recipe/get-medicines');
  }
  
}
