export interface DoctorAppoinmentsFilterDto {
    dateFrom: number;
    dateTo: number;
    pacientFirstName: string;
    pacientSecondName: string;
    statusId: number;
  }