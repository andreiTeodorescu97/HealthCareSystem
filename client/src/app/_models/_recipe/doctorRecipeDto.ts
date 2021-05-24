import { DateFormatter, DatepickerConfig } from "ngx-bootstrap/datepicker";

export interface DoctorRecipeDto {
    firstName: string;
    secondName: string;
    email: string;
    dateOfBirth: Date;
    mainPhotoUrl: string;
}