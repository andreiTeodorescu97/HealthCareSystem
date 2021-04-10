import { StudiesAndExperience } from "./studiesAndExperienceDto";

export interface DoctorDto {
    firstName: string;
    secondName: string;
    email: string;
    dateOfBirth: Date;
    age: number;
    studiesAndExperience: StudiesAndExperience[];
  }