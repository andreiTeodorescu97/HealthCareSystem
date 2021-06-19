export interface PacientHistoryDto {
    id: number;
    pacientId: number;
    firstName: string;
    secondName: string;
    email: string;
    gender: string;
    identityNumber: string;
    series: string;
    cnp: string;
    doctorId: number;
    dateOfBirth: Date;
    totalNumberOfVisits: number;
    lastVisitDate: Date;
}
