import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FullRecipeDto } from 'app/_models/_recipe/fullRecipeDto';
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
    return this.http.post(this.baseUrl + 'recipe/add-recipe', model);
  }

  getMedicines(){
    return this.http.get<MedicineDto[]>(this.baseUrl + 'recipe/get-medicines');
  }

  getRecipe(consultationId : number) {
    return this.http.get<FullRecipeDto>(this.baseUrl + 'recipe/get-recipe?consultationId=' + consultationId);
  }

  generatePDF(consultationId : number) {
    return this.http.get(this.baseUrl + 'recipe/generateRecipePdf?consultationId=' + consultationId, {responseType: 'arraybuffer'});
  }
  
}
