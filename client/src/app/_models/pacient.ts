import { PacientContactDto } from "./pacientContactDto";


export interface Pacient{
    firstName: string;
    secondName: string;
    email: string;
    gender: string;
    series: string;
    identityNumber: string;
    cnp: string;
    dateOfBirth: string;
    pacientContact: PacientContactDto;
}