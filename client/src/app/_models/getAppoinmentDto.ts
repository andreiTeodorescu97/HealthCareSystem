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
    pacientUserName: string;
    pacientFirstName: string;
    pacientSecondName: string;
    doctorId: number;
    doctorUserName: string;
    doctorFirstName: string;
    doctorSecondName: string;
    doctorProfilePhotoUrl: string;
    statusId: number;
    status: string;
    isConsultationAdded: boolean;
}