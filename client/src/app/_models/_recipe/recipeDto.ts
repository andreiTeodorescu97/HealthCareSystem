import { PrescriptionDto } from "./prescriptionDto";

export interface RecipeDto {
    id: number;
    uniqueId: string;
    dateAdded: string;
    consultationId: number;
    pacientId: number;
    prescriptions: PrescriptionDto[];
}