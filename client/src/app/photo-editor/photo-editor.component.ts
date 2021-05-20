import { Component, Input, OnInit } from '@angular/core';
import { DoctorDto } from 'app/_models/doctorDto';
import { PhotoDto } from 'app/_models/photoDto';
import { User } from 'app/_models/user';
import { UserDoctorDto } from 'app/_models/userDoctorDto';
import { AccountService } from 'app/_services/account.service';
import { DoctorService } from 'app/_services/doctor.service';
import { environment } from 'environments/environment';
import { FileUploader } from 'ng2-file-upload';
import { ToastrComponentlessModule, ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {

  @Input() doctor: DoctorDto;
  uploader: FileUploader;
  hasBaseDropzoneOver = false;
  baseUrl = environment.apiUrl;
  user: User;

  constructor(private accountService: AccountService, private toastr: ToastrService,
    private doctorService: DoctorService) {
    //gives acces to the current logged in user
    this.accountService.currentUser$.pipe(take(1)).subscribe(
      user => this.user = user);
  }

  ngOnInit(): void {
    this.initializeUploader(); 
  }

  fileOverBase(e: any) {
    this.hasBaseDropzoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'doctors/add-photo',
      authToken: 'Bearer ' + this.user.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    }

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const photo: PhotoDto = JSON.parse(response);
        this.doctor.photos.push(photo);
        if (photo.isMain) {
          this.user.mainPhotoUrl = photo.url;
          /* this.member.photoUrl = photo.url; */
          this.accountService.setCurrentUser(this.user);
        }
        this.toastr.success(
          '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Fotografia a fost salvata cu succes!</span>',
          "Update",
          {
            toastClass: "alert alert-success alert-with-icon",
          }
        );
      }
    }
  }

  setMainPhoto(photo: PhotoDto) {
    this.doctorService.setMainPhoto(photo.id).subscribe(() => {
      this.user.mainPhotoUrl = photo.url;
      this.accountService.setCurrentUser(this.user);
/*       this.member.photoUrl = photo.url; */
      this.doctor.photos.forEach(p => {
        if (p.isMain) p.isMain = false;
        if (p.id === photo.id) {
          p.isMain = true;
        }
      })
      this.toastr.success(
        '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Fotografia a fost setata cu succes!</span>',
        "Update",
        {
          toastClass: "alert alert-success alert-with-icon",
        }
      );
    })
  }

  deletePhoto(photoId: number) {
    this.doctorService.deletePhoto(photoId).subscribe(() => {
      this.doctor.photos = this.doctor.photos.filter(x => x.id !== photoId);
      this.toastr.success(
        '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Fotografia a fost stearsa cu succes!</span>',
        "Update",
        {
          toastClass: "alert alert-success alert-with-icon",
        }
      );
    })
  }


}
