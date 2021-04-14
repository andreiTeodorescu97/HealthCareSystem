import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DoctorDto } from 'app/_models/doctorDto';
import { DoctorService } from 'app/_services/doctor.service';

@Component({
  selector: 'app-doctor-detail',
  templateUrl: './doctor-detail.component.html',
  styleUrls: ['./doctor-detail.component.css']
})
export class DoctorDetailComponent implements OnInit {

  doctor: DoctorDto;
  constructor(private doctorService: DoctorService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember(){
    this.doctorService.getDoctorByDoctorId(this.route.snapshot.paramMap.get('id')).subscribe(response => {
      this.doctor = response;
      this.doctor.studiesAndExperience.forEach(study => {
        study.startDate = new Date(study.startDate);
        study.endDate = new Date(study.endDate);
      });
    })
  }

  addAppoinment(){

  }

}
