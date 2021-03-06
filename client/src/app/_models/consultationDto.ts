export interface ConsultationDto {

    id: number;
    height: number;
    weight: number;
    systolicBp: number;
    diastolicBp: number;
    temperature: number;
    heartRate: number;
    bloodSugar: number;
    bmi: number;
    respiratoryRate: number;
    numberOfCigarettesPerDay: number;
    generalFeeling: string;
    comments: string;
    appoinmentId: number;
    pacientId: number;
    dateAdded: Date;
    hasRecipe: boolean;
}