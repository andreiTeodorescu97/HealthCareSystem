export interface MakeAnAppoinmentDto {
    doctorId: number;
    dayUnixTime: number;
    fromTimeSpan: number;
    toTimeSpan: number;
    reason: string;
}