import { PhotoDto } from "./photoDto";
import { StudiesAndExperience } from "./studiesAndExperienceDto";
import { WorkDayDto } from "./workDayDto";

export interface DoctorDto {
    firstName: string;
    secondName: string;
    email: string;
    dateOfBirth: Date;
    age: number;
    mainPhotoUrl: string;
    studiesAndExperience: StudiesAndExperience[];
    workDays: WorkDayDto[];
    photos: PhotoDto[];
  }