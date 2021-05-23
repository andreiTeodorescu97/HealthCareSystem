import { MedicineDto } from "./medicineDto";

export interface PrescriptionDto {
    dosageType: string;
    dosageNumberPerDay: number;
    frequency: number;
    foodRelation: string;
    numberOfDays: number;
    route: string;
    instructions: string;
    medicineId: number;
    medicine: MedicineDto;
}