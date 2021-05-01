import { PacientContactDto } from "./pacientContactDto";
import { PacientGeneralMedicalDataDto } from "./pacientGeneralMedicalDataDto";

export interface Pacient{
    firstName: string;
    secondName: string;
    email: string;
    gender: string;
    series: string;
    identityNumber: string;
    cnp: string;
    dateOfBirth: string;
    age: number;
    pacientContact: PacientContactDto;
    pacientGeneralMedicalData: PacientGeneralMedicalDataDto;
}