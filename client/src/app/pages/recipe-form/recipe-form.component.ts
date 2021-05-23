import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MedicineDto } from 'app/_models/_recipe/medicineDto';
import { RecipeService } from 'app/_services/recipe.service';

@Component({
  selector: 'app-recipe-form',
  templateUrl: './recipe-form.component.html',
  styleUrls: ['./recipe-form.component.css']
})
export class RecipeFormComponent implements OnInit {

  consultationId: string;
  medicines: MedicineDto[];

  constructor(private route: ActivatedRoute, private recipeService: RecipeService) {
    this.consultationId = this.route.snapshot.paramMap.get('id'); 
  }

  ngOnInit(): void {

  }

  getMedicines(){
    this.recipeService.getMedicines().subscribe(response => {
      this.medicines = response;
    })
  }

}
