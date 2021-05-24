import { DoctorRecipeDto } from "./doctorRecipeDto";
import { PacientRecipeDto } from "./pacientRecipeDto";
import { RecipeDto } from "./recipeDto";

export interface FullRecipeDto {
    doctor: DoctorRecipeDto;
    pacient: PacientRecipeDto;
    recipe: RecipeDto;
}