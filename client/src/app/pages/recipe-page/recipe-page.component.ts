import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FullRecipeDto } from 'app/_models/_recipe/fullRecipeDto';
import { RecipeService } from 'app/_services/recipe.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-recipe-page',
  templateUrl: './recipe-page.component.html',
  styleUrls: ['./recipe-page.component.css']
})
export class RecipePageComponent implements OnInit {

  consultationId: string;
  fullRecipeInfo: FullRecipeDto;

  constructor(private route: ActivatedRoute,
    private recipeService: RecipeService,
    private router: Router,
    private toastr: ToastrService) { 
      this.consultationId = this.route.snapshot.paramMap.get('consultationId');
    }

  ngOnInit(): void {
    this.getRecipe();
  }

  getRecipe(){
    this.recipeService.getRecipe(+this.consultationId).subscribe(data => {
      this.fullRecipeInfo = data;
    })
  }

}
