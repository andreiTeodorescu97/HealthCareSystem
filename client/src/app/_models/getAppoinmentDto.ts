export interface GetAppoinmentDto {
    id: number;
    appoinmentDate: string;
    appoinmentHour: string;
    appoinmentStartSpan: number;
    appoinmentEndSpan: number;
    reason: string;
    dateId: number;
    dateCreated: Date;
    pacientId: number;
    pacientFirstName: string;
    pacientSecondName: string;
    doctorId: number;
    doctorFirstName: string;
    doctorSecondName: string;
    statusId: number;
    status: string;
    isConsultationAdded: boolean;
}