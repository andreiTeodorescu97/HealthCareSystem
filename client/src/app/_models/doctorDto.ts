import { StudiesAndExperience } from "./studiesAndExperienceDto";
import { WorkDayDto } from "./workDayDto";

export interface DoctorDto {
    firstName: string;
    secondName: string;
    email: string;
    dateOfBirth: Date;
    age: number;
    studiesAndExperience: StudiesAndExperience[];
    workDays: WorkDayDto[];
  }