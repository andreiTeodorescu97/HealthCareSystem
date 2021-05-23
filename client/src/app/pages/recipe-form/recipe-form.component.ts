import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgSelectConfig } from '@ng-select/ng-select';
import { MedicineDto } from 'app/_models/_recipe/medicineDto';
import { PrescriptionDto } from 'app/_models/_recipe/prescriptionDto';
import { RecipeDto } from 'app/_models/_recipe/recipeDto';
import { RecipeService } from 'app/_services/recipe.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-recipe-form',
  templateUrl: './recipe-form.component.html',
  styleUrls: ['./recipe-form.component.css']
})
export class RecipeFormComponent implements OnInit {

  consultationId: string;
  pacientFirstName: string;
  pacientSecondName: string;
  pacientId: string;

  medicines: MedicineDto[];
  selectedMedicine = {} as MedicineDto;
  recipe = {} as RecipeDto;

  newPrescription = { dosageType: '', dosageNumberPerDay: '', frequency: '', foodRelation: '', numberOfDays: '', route: '', instructions: '', medicineId: '', medicine: {} } as unknown as PrescriptionDto;

  dosageTypes = [
    { id: 1, name: 'Comprimat' },
    { id: 2, name: 'Fiola' },
    { id: 3, name: 'Drajeu' },
    { id: 4, name: 'Sirop' },
    { id: 5, name: 'Supozitor' },
    { id: 7, name: 'Spray' },
    { id: 8, name: 'Plic' },
    { id: 9, name: 'Capsula' },
    { id: 11, name: 'Unguent' },
    { id: 13, name: 'Crema' },
    { id: 14, name: 'Gel' },
    { id: 18, name: 'Seringi' },
    { id: 19, name: 'Inhalant' },
    { id: 6, name: 'Pulbere injectabila' },
    { id: 10, name: 'Comprimat efervescent' },
    { id: 12, name: 'Comprimat pentru supt' },
    { id: 15, name: 'Solutie picaturi' },
    { id: 16, name: 'Solutie oftalmologica' },
    { id: 17, name: 'Solutie nazala' },
  ];

  routeTypes = [
    { id: 1, name: 'Oral' },
    { id: 3, name: 'Vaginal' },
    { id: 4, name: 'Uretral' },
    { id: 4, name: 'Cutanat' },
    { id: 5, name: 'Subcutanat' },
    { id: 5, name: 'Intravenos' },
    { id: 2, name: 'Intrarectal' },
    { id: 5, name: 'Intramuscular' },
  ];

  prescriptionsList = [] as PrescriptionDto[];

  constructor(private route: ActivatedRoute,
    private recipeService: RecipeService,
    private selectizeConfig: NgSelectConfig,
    private router: Router,
    private toastr: ToastrService) {
    this.consultationId = this.route.snapshot.paramMap.get('consultationId');
    this.pacientId = this.route.snapshot.paramMap.get('pacientId');
    this.pacientFirstName = this.route.snapshot.paramMap.get('pacientFirstName');
    this.pacientSecondName = this.route.snapshot.paramMap.get('pacientSecondName');
    this.selectizeConfig.notFoundText = 'Nu exista inregistrari!';
  }

  ngOnInit(): void {
    this.getMedicines();
  }

  getMedicines() {
    this.recipeService.getMedicines().subscribe(response => {
      this.medicines = response;
    })
  }

  onChangeMedicine($event) {
    this.newPrescription.medicineId = $event.id;
  }

  addPrescriptionToList(prescriptionToAdd: PrescriptionDto) {
    if (this.validatePrescription(prescriptionToAdd)) {
      this.prescriptionsList.push(prescriptionToAdd);
      this.resetPrescriptionForm();
    } else {
      this.toastr.error(
        '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Va rugam completati toate campurile obligatorii, marcate cu (*)!</span>',
        "Formular prescriptie",
        {
          toastClass: "alert alert-danger alert-with-icon",
        }
      );
    }
  }

  deletePrescription(prescription: PrescriptionDto) {
    const index: number = this.prescriptionsList.indexOf(prescription);
    if (index !== -1) {
      this.prescriptionsList.splice(index, 1);
    }
  }

  validatePrescription(prescriptionToAdd: PrescriptionDto): boolean {
    if (prescriptionToAdd == null || prescriptionToAdd.dosageNumberPerDay == +"" ||
      prescriptionToAdd.dosageType == "" ||
      prescriptionToAdd.frequency == +"" ||
      prescriptionToAdd.route == "" ||
      prescriptionToAdd.medicineId == +"") {
      return false
    }
    return true;
  };

  resetPrescriptionForm() {
    this.newPrescription = { dosageType: '', dosageNumberPerDay: '', frequency: '', foodRelation: '', numberOfDays: '', route: '', instructions: '', medicineId: '', medicine: {} } as unknown as PrescriptionDto;;
  }

  resetRecipe() {
    this.prescriptionsList = [];
  }

  saveRecipe() {
    this.recipe.pacientId = +this.pacientId;
    this.recipe.consultationId = +this.consultationId;
    this.recipe.prescriptions = this.prescriptionsList;

    this.recipeService.addRecipe(this.recipe).subscribe(() => {
      this.toastr.success(
        '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Reteta a fost salvata cu success!</span>',
        "General",
        {
          toastClass: "alert alert-success alert-with-icon",
        }
      );
      this.redirectToPacientProfile();
    })
  }

  redirectToPacientProfile() {
    this.router.navigateByUrl('pacient/pacient_profile/' + this.pacientId);
  }

}
