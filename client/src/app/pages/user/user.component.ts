import { DatePipe } from '@angular/common';
import { AbstractType, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { City } from 'app/_models/city';
import { Pacient } from 'app/_models/pacient';
import { Region } from 'app/_models/region';
import { User } from 'app/_models/user';
import { AccountService } from 'app/_services/account.service';
import { CityService } from 'app/_services/city.service';
import { PacientService } from 'app/_services/pacient.service';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';

@Component({
    selector: 'user-cmp',
    moduleId: module.id,
    templateUrl: 'user.component.html'
})

export class UserComponent implements OnInit {
    model: Pacient;
    user: User;
    cities: City[];
    citiesReplica: City[];
    regions: Region[];
    dateFormated: any;
    editForm: FormGroup;

    constructor(private pacientService: PacientService, private accountService: AccountService, private cityService: CityService, private toastr:ToastrService) {
        this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
    }

    ngOnInit(): void {
        this.getPacient();
        this.getCities();
        this.getRegions();
        this.initializeForm();
        this.editForm.get("pacientContact.regionId").valueChanges.subscribe(newRegion => {
          this.model.pacientContact.cityId = null;
          this.cities = this.citiesReplica.filter(city => city.regionId == newRegion);
          this.model.pacientContact.regionId = newRegion;
          this.editForm.patchValue (this.model, {emitEvent:false});
        })
    }

    initializeForm() {
        this.editForm = new FormGroup({
            firstName: new FormControl('', Validators.required),
            secondName: new FormControl('', Validators.required),
            email: new FormControl('', [Validators.required,Validators.pattern('^[^\\s@]+@[^\\s@]+\\.[^\\s@]{2,}$')]),
            series: new FormControl(),
            dateOfBirth : new FormControl(),
            identityNumber: new FormControl(),
            cnp: new FormControl(),
            pacientContact: new FormGroup({
                firstPhone : new FormControl(),
                secondPhone : new FormControl(),
                street : new FormControl(),
                streetNumber : new FormControl(),
                regionId : new FormControl(),
                cityId : new FormControl(),
            })
        });
    }

    getPacient() {
        this.pacientService.getPacientByCnp(this.user.cnp)
            .subscribe(response => {
                this.model = response;
                this.editForm.patchValue (this.model, {emitEvent:false});
                this.dateFormated = this.model.dateOfBirth;
                var userdate: any = new Date(this.dateFormated);
                var datePipe = new DatePipe('en-US');
                this.dateFormated = datePipe.transform(userdate, 'dd.MMM.yyyy');
            });
    }

    getCities() {
        this.cityService.getCities().subscribe(response => {
            this.cities = response;
            this.citiesReplica = response;
        })
    }

    getRegions() {
        this.cityService.getRegions().subscribe(response => {
            this.regions = response;
        })
    }

    updatePacient(){
        console.log(this.editForm.value);
        this.pacientService.updatePacient(this.editForm.value).subscribe(() => {
          this.toastr.success(
            '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Profilul a fost actualizat cu succes!</span>',
            "Update",
            {
              toastClass: "alert alert-success alert-with-icon",
            }
          );
          this.user.firstName = this.editForm.value.firstName;
          this.user.secondName = this.editForm.value.secondName;
          this.accountService.setCurrentUser(this.user);
        })
      }

      regionChange(): void{
        this.model.pacientContact.cityId = null;
        this.cities = this.citiesReplica.filter(city => city.regionId == this.model.pacientContact.regionId);
      }

      onRegionChange():void {

      }
}
